using System.Threading.Tasks;
using Acme.BookStore.Books;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace TodoApp;

[DependsOn(
    typeof(TodoAppDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(TodoAppApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
[DependsOn(typeof(AbpBackgroundWorkersModule))]

public class TodoAppApplicationModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.Configure<AbpBackgroundWorkerOptions>(options =>
        {
            options.IsEnabled = true; // Bật hoặc tắt hệ thống background worker
        
        });
    }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<TodoAppApplicationModule>();
        });
    }
    public override async Task OnApplicationInitializationAsync(
       ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<MyLogWorker>();
      

    }

     
}
