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

public class ProxyRepository
    : EfCoreRepository<TodoAppDbContext, Proxies, Guid>, IProxyRepository

{
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public ProxyRepository(IDbContextProvider<TodoAppDbContext> dbContextProvider,
    IUnitOfWorkManager unitOfWorkManager) : base(dbContextProvider)
    {
        _unitOfWorkManager = unitOfWorkManager;
    }

    public async Task<bool> CheckHostExistAsync(string host, int? port)
    {
        var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true);
        try
        {
            if (host.IsNullOrEmpty() || port == 0)
            {
                return true;
            }
            // Sử dụng GetDbContextAsync() và AnyAsync() để kiểm tra không đồng bộ
            // Get the DbContext from DI or similar method
            var dbContext = await GetDbContextAsync();
            if (dbContext == null)
            {
                Logger.LogError("DbContext is null.");
                return false;
            }
            var exists = await dbContext.Proxies.AnyAsync(x => x.Port == port && x.Host == host);

            await uow.CompleteAsync();

            return exists == true ? true : false;
        }
        catch (System.Exception ex)
        {
            Logger.LogError($"Error checking host existence: {ex.Message}");
            await uow.RollbackAsync();

            return true;
        }
        
    }
}


