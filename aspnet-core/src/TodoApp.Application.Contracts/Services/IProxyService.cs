using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TodoApp
{
    public interface IProxyService : IApplicationService
    {
        Task<bool> CheckProxyAsync(CheckProxyDto input);
        Task<PagedResultDto<ProxyDto>> CrawlProxiesFromUrlAsync(CrawlProxyDto input);
    }
}
