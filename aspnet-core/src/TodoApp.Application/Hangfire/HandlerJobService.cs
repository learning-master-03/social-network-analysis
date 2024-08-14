using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hangfire.Console;
using Hangfire.RecurringJobExtensions;
using Hangfire.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TodoApp;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace Acme.BookStore.Books
{


    public class HandlerJobService : ApplicationService, ITransientDependency
    {
        private readonly ILogger<HandlerJobService> _logger;
        private readonly IProxyJobRepository _repository;
        private readonly IProxyRepository _proxyRepository;

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ProxyAppService _proxyService;

        public HandlerJobService(ILogger<HandlerJobService> logger,
        IProxyJobRepository repository,
        IUnitOfWorkManager unitOfWorkManager,
        ProxyAppService proxyService,
        IProxyRepository proxyRepository)
        {
            _logger = logger;
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _proxyService = proxyService;
            _proxyRepository = proxyRepository;
        }
        // [RecurringJob("JobPullIPAsync", Cron = "*/15 * * * *", TimeZone = "Asia/Bangkok", RecurringJobId = "Job Auto Pull Data IP From Github")]
        // public async Task JobPullIPAsync(PerformContext context)
        // {
        //     // var uow = _unitOfWorkManager.Begin(isTransactional: true, requiresNew: true);
        //     try
        //     {
        //         var proxyJobConfigs = await _repository.GetListAsync(x => x.IsActive);
        //         foreach (var config in proxyJobConfigs)
        //         {
        //             // Set the default timeRun value
        //             double timeRun = config.TimeRun ?? 600000; // Default to 600000 if not set

        //             // Ensure LastModificationTime is available
        //             if (!config.LastModificationTime.HasValue)
        //             {
        //                 context.WriteLine($"Skipping job for config {JsonConvert.SerializeObject(config)}: LastModificationTime is not set.");
        //                 continue; // Skip this iteration if LastModificationTime is not available
        //             }

        //             // Calculate the time difference
        //             var lastTime = config.LastModificationTime.Value;
        //             var currentTime = DateTime.UtcNow;
        //             var timeDifference = TimeSpan.FromTicks(currentTime.Ticks - lastTime.Ticks).TotalMinutes;

        //             // Check if the time difference equals the specified TimeRun
        //             // Here, we compare the total milliseconds of the timeDifference with timeRun
        //             if (timeDifference >= timeRun)
        //             {
        //                 context.WriteLine($"Run Job : {JsonConvert.SerializeObject(config)}");

        //                 // var updateConfig = await _repository.FirstAsync(x => x.Id == config.Id);
        //                 // if (updateConfig == null) continue;

        //                 config.LastModificationTime = currentTime;
        //                 config.Version += 1;
        //                 await _repository.UpdateAsync(config);


        //                 //Process Crawl
        //                 var isSuccess = await _proxyService.CrawlProxiesFromUrlAsync(new CrawlProxyDto()
        //                 {
        //                     Id = config.Id,
        //                     Url = config.Url,
        //                     IsRawUrl = config.IsRawUrl,
        //                     FileType = config.FileType,
        //                     ProxyType = config.ProxyType

        //                 });
        //                 if (isSuccess)
        //                 {
        //                     context.WriteLine($"Handling Id [{config.Id}] crawling success");
        //                 }
        //             }
        //         }
        //         // await uow.CompleteAsync();
        //         await Task.CompletedTask;

        //     }
        //     catch (System.Exception ex)
        //     {
        //         // await uow.RollbackAsync();
        //         await Task.CompletedTask;

        //     }

        // }


        [RecurringJob("JobCheckActiveIPUseSemaphoreSlim", Cron = "*/30 * * * *", TimeZone = "Asia/Bangkok", RecurringJobId = "Job Auto Check IP Is Active Use SemaphoreSlim")]
        public async Task JobCheckIPSemaphoreSlimAsync(PerformContext context)
        {
            try
            {
                context.WriteLine($"Begin Handling JobCheckIPAsync...");
                // Lấy thông tin về Thread Pool
                ThreadPool.GetMaxThreads(out int maxWorkerThreads, out _);

                // Tính toán 70% sức mạnh server
                int maxDegreeOfParallelism = (int)(maxWorkerThreads * 0.7); // Lấy 70% maxWorkerThreads

                // Ghi lại thời gian bắt đầu
                var startTime = DateTime.UtcNow;
                var hosts = await _proxyRepository.GetListAsync();
                int totalCount = hosts.Count;
                int completedCount = 0; // Biến theo dõi số lượng tác vụ đã hoàn thành

                var tasks = new List<Task>();
                var semaphore = new SemaphoreSlim(maxDegreeOfParallelism);
                var lockObject = new object(); // Đối tượng khóa để đồng bộ hóa
                var bar = context.WriteProgressBar();

                foreach (var item in hosts.WithProgress(bar))
                {
                    // Chờ cho đến khi có chỗ trống trong semaphore
                    await semaphore.WaitAsync();

                    // Thêm từng tác vụ kiểm tra vào danh sách
                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            int currentItem; // Biến tạm thời để lưu index

                            // Để có được chỉ số hoàn thành mà không bị xung đột
                            lock (lockObject)
                            {
                                currentItem = completedCount + 1; // Tính toán chỉ số cho log
                            }

                            var isCheck = await _proxyService.CheckProxyAsync(new CheckProxyDto()
                            {
                                Host = item.Host,
                                Port = item.Port ?? 0,
                                ProxyType = item.ProxyType
                            });

                            if (isCheck)
                            {
                                lock (context) // Đảm bảo ghi log an toàn khi đa luồng
                                {
                                    context.WriteLine($"Result CheckProxyAsync for {item.Host}:{item.Port} => {isCheck}");
                                }

                                item.IsCanUsed = true;
                                item.LastModificationTime = DateTime.UtcNow;

                                await _proxyRepository.UpdateAsync(item, autoSave: true);
                            }
                        }
                        finally
                        {
                            // Giải phóng semaphore để cho phép tác vụ khác chạy
                            semaphore.Release();

                            // Cập nhật số lượng tác vụ đã hoàn thành
                            lock (lockObject) // Đảm bảo cập nhật số lượng an toàn khi đa luồng
                            {
                                completedCount++;
                            }

                        }
                    }));

                }
                // Tính toán thời gian hoàn thành
                var endTime = DateTime.UtcNow;
                var duration = endTime - startTime;

                // In ra thời gian hoàn thành công việc
                context.WriteLine($"Job completed in {duration.TotalMilliseconds} seconds. Total completed: {completedCount}/{totalCount}");
                // Đợi tất cả các tác vụ hoàn thành
                await Task.WhenAll(tasks);
            }
            catch (System.Exception ex)
            {
                context.WriteLine($"An error occurred: {ex.Message}");
                await Task.CompletedTask;
            }
        }


        // [RecurringJob("JobCheckActiveIP", Cron = "*/15 * * * *", TimeZone = "Asia/Bangkok", RecurringJobId = "Job Auto Check IP Is Active")]
        // public async Task JobCheckIPAsync(PerformContext context)
        // {


        //     context.WriteLine($"Begin Handling JobCheckIPAsync...");
        //     // Ghi lại thời gian bắt đầu
        //     var startTime = DateTime.UtcNow;
        //     var hosts = await _proxyRepository.GetListAsync();
        //     int totalCount = hosts.Count;
        //     int completedCount = 0; // Biến theo dõi số lượng tác vụ đã hoàn thành

        //     var bar = context.WriteProgressBar();

        //     foreach (var item in hosts.WithProgress(bar))
        //     {


        //         var isCheck = await _proxyService.CheckProxyAsync(new CheckProxyDto()
        //         {
        //             Host = item.Host,
        //             Port = item.Port ?? 0,
        //             ProxyType = item.ProxyType
        //         });

        //         if (isCheck)
        //         {

        //             context.WriteLine($"Result CheckProxyAsync for {item.Host}:{item.Port} => {isCheck}");

        //             item.IsCanUsed = true;
        //             item.LastModificationTime = DateTime.UtcNow;

        //             await _proxyRepository.UpdateAsync(item, autoSave: true);
        //         }
        //         completedCount++;


        //     }


        //     // Tính toán thời gian hoàn thành
        //     var endTime = DateTime.UtcNow;
        //     var duration = endTime - startTime;

        //     // In ra thời gian hoàn thành công việc
        //     context.WriteLine($"Job completed in {duration.TotalMilliseconds} seconds. Total completed: {completedCount}/{totalCount}");
        //     // Đợi tất cả các tác vụ hoàn thành
        //     await Task.CompletedTask;

        // }
    }
}
