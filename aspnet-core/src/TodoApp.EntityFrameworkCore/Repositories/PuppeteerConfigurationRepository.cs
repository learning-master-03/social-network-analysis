using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TodoApp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;

namespace TodoApp.EntityFrameworkCore;

public class PuppeteerConfigurationRepository
    : EfCoreRepository<TodoAppDbContext, PuppeteerConfiguration, Guid>, IPuppeteerConfigurationRepository

{
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public PuppeteerConfigurationRepository(IDbContextProvider<TodoAppDbContext> dbContextProvider,
    IUnitOfWorkManager unitOfWorkManager) : base(dbContextProvider)
    {
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task<PuppeteerConfiguration?> GetConfigurationPuppeteerAsync()
    {
        try
        {
            var dbContext = await GetDbContextAsync();

            return await dbContext.PuppeteerConfigurations.FirstAsync();
        }
        catch (System.Exception)
        {

            return null;
        }
    }


}


