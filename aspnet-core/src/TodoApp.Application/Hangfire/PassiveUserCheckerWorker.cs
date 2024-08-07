// using System.Threading.Tasks;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Logging;
// using Volo.Abp.BackgroundWorkers;
// using Volo.Abp.Threading;
// using Volo.Abp.Users;

// public class PassiveUserCheckerWorker : AsyncPeriodicBackgroundWorkerBase
// {
//     public PassiveUserCheckerWorker(
//             AbpAsyncTimer timer,
//             IServiceScopeFactory serviceScopeFactory
//         ) : base(
//             timer, 
//             serviceScopeFactory)
//     {
//         Timer.Period = 30000; //10 minutes
//     }

//     protected async override Task DoWorkAsync(
//         PeriodicBackgroundWorkerContext workerContext)
//     {
//         Logger.LogInformation("Starting: Setting status of inactive users...");

//         // //Resolve dependencies
//         // var userRepository = workerContext
//         //     .ServiceProvider
//         //     .GetRequiredService<IUserRepository>();

//         // //Do the work
//         // await userRepository.UpdateInactiveUserStatusesAsync();

//         Logger.LogInformation("Completed: Setting status of inactive users...");
//     }
// }