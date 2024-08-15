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
        private readonly DateTime startTime = DateTime.UtcNow;

        private readonly ILogger<HandlerJobService> _logger;
        private readonly IProxyJobRepository _repository;
                private readonly ITikiCategoryRepository _tikiCategoriesRepository;
        private readonly IProxyRepository _proxyRepository;

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ProxyAppService _proxyService;
                private readonly PuppeteerService _puppeteerService;


        public HandlerJobService(ILogger<HandlerJobService> logger,
        IProxyJobRepository repository,
        IUnitOfWorkManager unitOfWorkManager,
        ProxyAppService proxyService,
        IProxyRepository proxyRepository,
        ITikiCategoryRepository tikiCategoriesRepository,
        PuppeteerService puppeteerService)
        {
            _logger = logger;
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
            _proxyService = proxyService;
            _proxyRepository = proxyRepository;
_tikiCategoriesRepository = tikiCategoriesRepository;
_puppeteerService = puppeteerService;
        }
        [RecurringJob("JobPullIPAsync", Cron = "*/15 * * * *", TimeZone = "Asia/Bangkok", RecurringJobId = "Job Auto Pull Data IP From Github")]
        public async Task JobPullIPAsync(PerformContext context)
        {
            try
            {
                var bar = context.WriteProgressBar();
                // Lấy thông tin về Thread Pool
                ThreadPool.GetMaxThreads(out int maxWorkerThreads, out _);

                // Tính toán 70% sức mạnh server
                int maxDegreeOfParallelism = (int)(maxWorkerThreads * 0.3); // Lấy 70% maxWorkerThreads

                var tasks = new List<Task>();
                // var semaphore = new SemaphoreSlim(maxDegreeOfParallelism);
                var semaphore = new SemaphoreSlim(3, 10);
                var lockObject = new object(); // Đối tượng khóa để đồng bộ hóa
                var proxyJobConfigs = await _repository.GetListAsync(x => x.IsActive);
                int totalCount = proxyJobConfigs.Count;

                int completedCount = 0; // Biến theo dõi số lượng tác vụ đã hoàn thành

                foreach (var config in proxyJobConfigs.WithProgress(bar))
                {
                    await semaphore.WaitAsync();
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
                            // Set the default timeRun value
                            double timeRun = config.TimeRun ?? 600000; // Default to 600000 if not set

                            // Ensure LastModificationTime is available
                            if (!config.LastModificationTime.HasValue)
                            {
                                context.WriteLine($"Skipping job for config {JsonConvert.SerializeObject(config)}: LastModificationTime is not set.");
                                return; // Skip this iteration if LastModificationTime is not available
                            }

                            // Calculate the time difference
                            var lastTime = config.LastModificationTime.Value;
                            var currentTime = DateTime.UtcNow;
                            var timeDifference = TimeSpan.FromTicks(currentTime.Ticks - lastTime.Ticks).TotalMinutes;

                            // Check if the time difference equals the specified TimeRun
                            // Here, we compare the total milliseconds of the timeDifference with timeRun
                            if (timeDifference >= timeRun)
                            {
                                // context.WriteLine($"Run Job : {JsonConvert.SerializeObject(config)}");

                                // var updateConfig = await _repository.FirstAsync(x => x.Id == config.Id);
                                // if (updateConfig == null) continue;

                                config.LastModificationTime = currentTime;
                                config.Version += 1;
                                await _repository.UpdateAsync(config);


                                //Process Crawl
                                var isSuccess = await _proxyService.CrawlProxiesFromUrlAsync(new CrawlProxyDto()
                                {
                                    Id = config.Id,
                                    Url = config.Url,
                                    IsRawUrl = config.IsRawUrl,
                                    FileType = config.FileType,
                                    ProxyType = config.ProxyType

                                });
                                if (isSuccess)
                                {
                                    // context.WriteLine($"Handling Id [{config.Id}] crawling success");
                                }
                            }
                        }
                        finally
                        {
                            semaphore.Release();
                            lock (lockObject) // Đảm bảo cập nhật số lượng an toàn khi đa luồng
                            {
                                completedCount++;
                            }
                        }
                    }));


                }
                await Task.WhenAll(tasks);

                var endTime = DateTime.UtcNow;
                var duration = endTime - startTime;

                // In ra thời gian hoàn thành công việc
                context.WriteLine($"Job completed in {duration.TotalMinutes} minutes. Total completed: {completedCount}/{totalCount}");
                // Đợi tất cả các tác vụ hoàn thành

            }
            catch (System.Exception ex)
            {
                context.WriteLine($"Finish Handling JobPullIPAsync Exception: {ex.Message}");

                await Task.CompletedTask;

            }

        }


        [RecurringJob("JobCheckActiveIPUseSemaphoreSlim", Cron = "*/30 * * * *", TimeZone = "Asia/Bangkok", RecurringJobId = "Job Auto Check IP Is Active Use SemaphoreSlim")]
        public async Task JobCheckIPSemaphoreSlimAsync(PerformContext context)
        {
            try
            {
                context.WriteLine($"Begin Handling JobCheckIPAsync...");
                // Lấy thông tin về Thread Pool
                ThreadPool.GetMaxThreads(out int maxWorkerThreads, out _);

                // Tính toán 70% sức mạnh server
                int maxDegreeOfParallelism = (int)(maxWorkerThreads * 0.1); // Lấy 70% maxWorkerThreads

                // Ghi lại thời gian bắt đầu
                var hosts = await _proxyRepository.GetListAsync();
                int totalCount = hosts.Count;
                int completedCount = 0; // Biến theo dõi số lượng tác vụ đã hoàn thành

                var tasks = new List<Task>();
                var semaphore = new SemaphoreSlim(maxDegreeOfParallelism);
                // var semaphore = new SemaphoreSlim(3, 10);

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
                await Task.WhenAll(tasks);

                // Tính toán thời gian hoàn thành
                var endTime = DateTime.UtcNow;
                var duration = endTime - startTime;

                // In ra thời gian hoàn thành công việc
                context.WriteLine($"Job completed in {duration.TotalMinutes} minutes. Total completed: {completedCount}/{totalCount}");
                // Đợi tất cả các tác vụ hoàn thành
            }
            catch (System.Exception ex)
            {
                context.WriteLine($"Finish Handling JobCheckIPSemaphoreSlimAsync Exception: {ex.Message}");
                await Task.CompletedTask;
            }
        }
        [RecurringJob("JobPullProductLinksOfTiki", Cron = "* */23 * * *", TimeZone = "Asia/Bangkok", RecurringJobId = "Job Pull Product Link From Tiki")]
          public async Task JobPullProductLinksFromTikisync(PerformContext context){
            try
            {
                var categories = await _tikiCategoriesRepository.GetListAsync();
                var bar = context.WriteProgressBar();
                foreach (var category in categories.WithProgress(bar)){
                    await _puppeteerService.CrawlProductByUrlAsync(category.Url);
                }
                var endTime = DateTime.UtcNow;
                var duration = endTime - startTime;

                // In ra thời gian hoàn thành công việc
                context.WriteLine($"Job completed in {duration.TotalMinutes} minutes.");
                // Đợi tất cả các tác vụ hoàn thành
            }
            catch (System.Exception ex)
            {
                                context.WriteLine($"Finish Handling JobPullProductLinksFromTikisync Exception: {ex.Message}");

                await Task.CompletedTask;
            }
          }

    }
}
