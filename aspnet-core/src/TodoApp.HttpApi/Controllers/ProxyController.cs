// using System.Threading.Tasks;
// using Acme.BookStore.Books;
// using Microsoft.AspNetCore.Mvc;
// using Volo.Abp.Application.Dtos;

// namespace TodoApp.Controllers;

// /* Inherit your controllers from this class.
//  */
// [Route("api/proxy")]
// [ApiExplorerSettings(GroupName = TodoAppHttpApiModule.ProxyGroup)]

// public class ProxyController : TodoAppController
// {
//     private readonly IProxyService _service;
//     protected ProxyController(IProxyService service)
//     {
//         _service = service;
//     }
//     [HttpGet("check-proxy-async")]
//     public async Task<bool> CheckProxyAsync([FromBody] CheckProxyDto input) => await _service.CheckProxyAsync(input);
//     [HttpPost("crawl-proxy-from-url-async")]
//     public async Task<PagedResultDto<ProxyDto>> CrawlProxiesFromUrlAsync([FromBody] CrawlProxyDto input) => await _service.CrawlProxiesFromUrlAsync(input);
// }
