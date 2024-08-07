using Acme.BookStore;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace TodoApp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TodoAppEntityFrameworkCoreModule),
    typeof(TodoAppApplicationContractsModule)
    )]
public class TodoAppDbMigratorModule : AbpModule
{
//       public override void ConfigureServices(ServiceConfigurationContext context)
//     {
//         // Register the data seeder
//         context.Services.AddTransient<IDataSeedContributor, BookStoreDataSeederContributor>();
//     }
}
