using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace TodoApp
{
    public interface IPuppeteerService : IApplicationService
    {
        Task CreateAsync();
        Task CrawlProductAsync(Guid categoryId);
        Task CrawlProductByUrlAsync(string url);

        Task CrawlProductLinkAsync(Guid categoryId);
        Task CrawlProductReviewAsync(Guid productId);

    }
}
