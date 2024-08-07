using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;

namespace Acme.BookStore.Books
{
    [BackgroundWorkerName("NewWorkerName")] // Thay đổi tên worker
    [BackgroundJobName("NewWorkerJobName")] // Tên job mới nếu cần
    [Display(Name = "NewWorkerName")]
    [DisplayName("Send order #{0} to warehouse")]

    public class MyLogWorker : AsyncPeriodicBackgroundWorkerBase
    {
        public MyLogWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
        {
            timer.Period = 5000000; // Đặt khoảng thời gian cho worker
        }
        [BackgroundWorkerName("NewWorkerName")] // Thay đổi tên worker
        [BackgroundJobName("NewWorkerJobName")] // Tên job mới nếu cần
        [Display(Name = "NewWorkerName")]
        [DisplayName("Send order #{0} to warehouse")]
        protected async override Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            Logger.LogInformation("Starting: MyLogWorker is running...");

            // Thực hiện công việc
            // Ví dụ: Cập nhật trạng thái người dùng không hoạt động, ghi log, v.v.

            Logger.LogInformation("Completed: MyLogWorker has finished the task.");
        }
    }
}
