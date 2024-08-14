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

public class ProxyJobRepository
    : EfCoreRepository<TodoAppDbContext, ProxyJobs, Guid>, IProxyJobRepository

{
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public ProxyJobRepository(IDbContextProvider<TodoAppDbContext> dbContextProvider,
    IUnitOfWorkManager unitOfWorkManager) : base(dbContextProvider)
    {
        _unitOfWorkManager = unitOfWorkManager;
    }

  
}


