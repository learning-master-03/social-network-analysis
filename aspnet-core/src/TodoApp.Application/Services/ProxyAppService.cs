using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PuppeteerSharp;
using TodoApp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Acme.BookStore.Books;

public class ProxyAppService : ApplicationService, IProxyService
{
    private readonly IRepository<PuppeteerConfiguration, Guid> _puppeteerConfigurationRepository;
    private readonly IProxyRepository _proxyRepository;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public ProxyAppService(IRepository<PuppeteerConfiguration, Guid> puppeteerConfigurationRepository,
                                        IUnitOfWorkManager unitOfWorkManager,
                                        IProxyRepository proxyRepository)
    {
        _puppeteerConfigurationRepository = puppeteerConfigurationRepository;
        _unitOfWorkManager = unitOfWorkManager;
        _proxyRepository = proxyRepository;
    }
    public async Task<bool> CheckProxyAsync(CheckProxyDto input)
    {
        switch (input.ProxyType)
        {
            case ProxyType.HTTP:
                return await CheckHTTPProxyAsync(input);
            case ProxyType.HTTPS:
                return await CheckHTTPSProxyAsync(input);
            case ProxyType.SOCKS4:
                return await CheckSOCKS4ProxyAsync(input);
            case ProxyType.SOCKS5:
                return await CheckSOCKS5ProxyAsync(input);
            default:
                throw new ArgumentException("");
        }

    }





    public async Task<bool> CheckHTTPProxyAsync(CheckProxyDto input)
    {
        // Kiểm tra nếu input không null và có giá trị hợp lệ
        if (input == null || string.IsNullOrWhiteSpace(input.Host) || input.Port <= 0)
        {
            return false;
        }
        try
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.Proxy = new WebProxy($"http://{input.Host}", input.Port);
                httpClientHandler.UseProxy = true;
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36");
                    httpClient.Timeout = TimeSpan.FromMilliseconds(2000);

                    HttpResponseMessage response = await httpClient.GetAsync("http://google.com");
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }
        catch (WebException ex)
        {
            // Ghi lại thông tin lỗi nếu cần
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // Ghi lại thông tin lỗi nếu cần
            Console.WriteLine($"General Error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> CheckHTTPSProxyAsync(CheckProxyDto input)
    {
        // Kiểm tra nếu input không null và có giá trị hợp lệ
        if (input == null || string.IsNullOrWhiteSpace(input.Host) || input.Port <= 0)
        {
            return false;
        }
        try
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.Proxy = new WebProxy($"https://{input.Host}", input.Port);
                httpClientHandler.UseProxy = true;
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36");
                    httpClient.Timeout = TimeSpan.FromMilliseconds(2000);

                    HttpResponseMessage response = await httpClient.GetAsync("http://google.com");
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }
        catch (WebException ex)
        {
            // Ghi lại thông tin lỗi nếu cần
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // Ghi lại thông tin lỗi nếu cần
            Console.WriteLine($"General Error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> CheckSOCKS4ProxyAsync(CheckProxyDto input)
    {
        // Kiểm tra nếu input không null và có giá trị hợp lệ
        if (input == null || string.IsNullOrWhiteSpace(input.Host) || input.Port <= 0)
        {
            return false;
        }
        try
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.Proxy = new WebProxy($"socks4://{input.Host}", input.Port);
                httpClientHandler.UseProxy = true;
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36");
                    httpClient.Timeout = TimeSpan.FromMilliseconds(2000);

                    HttpResponseMessage response = await httpClient.GetAsync("http://google.com");
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }
        catch (WebException ex)
        {
            // Ghi lại thông tin lỗi nếu cần
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // Ghi lại thông tin lỗi nếu cần
            Console.WriteLine($"General Error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> CheckSOCKS5ProxyAsync(CheckProxyDto input)
    {
        // Kiểm tra nếu input không null và có giá trị hợp lệ
        if (input == null || string.IsNullOrWhiteSpace(input.Host) || input.Port <= 0)
        {
            return false;
        }
        try
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.Proxy = new WebProxy($"socks5://{input.Host}", input.Port);
                httpClientHandler.UseProxy = true;
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.95 Safari/537.36");
                    httpClient.Timeout = TimeSpan.FromMilliseconds(2000);

                    HttpResponseMessage response = await httpClient.GetAsync("http://google.com");
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }
        catch (WebException ex)
        {
            // Ghi lại thông tin lỗi nếu cần
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // Ghi lại thông tin lỗi nếu cần
            Console.WriteLine($"General Error: {ex.Message}");
            return false;
        }
    }

    public async Task<PagedResultDto<ProxyDto>> CrawlProxiesFromUrlAsync(CrawlProxyDto input)
    {
        var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true);
        try
        {
            Logger.LogInformation($"Starting: CrawlProxiesFromUrlAsync... {input.Url} - {input.FileType}");
            var configPuppeteer = await _puppeteerConfigurationRepository.FirstAsync();

            //config puppeteer
            await new BrowserFetcher().DownloadAsync();
            var launchOptions = new LaunchOptions
            {
                Headless = configPuppeteer.Headless, // Chạy trình duyệt không ở chế độ headless để dễ dàng theo dõi
                Args = configPuppeteer.Args
            };

            using (var browser = await Puppeteer.LaunchAsync(launchOptions))
            using (var page = await browser.NewPageAsync())
            {
                try
                {
                    await page.GoToAsync(input.Url);
                    if (!input.IsRawUrl)
                    {
                        await page.WaitForSelectorAsync("a[data-testid='raw-button']");

                        await page.ClickAsync("a[data-testid='raw-button']");
                    }
                    await page.WaitForNavigationAsync();
                    // Lấy nội dung văn bản của trang
                    var bodyContent = await page.EvaluateExpressionAsync<string>("document.body.innerText");

                    var proxyList = bodyContent.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                                                .Select(p => p.Trim())
                                                .Where(p => !string.IsNullOrEmpty(p))
                                                .ToList();

                    var proxies = ExtractProxies(proxyList);

                    // Check proxy existence and create ProxyDto objects
                    var proxyTasks = proxies.Select(async p =>
                    {
                        var parts = p.Split(":");
                        if (parts.Length == 2)
                        {
                            var host = parts[0];
                            var port = int.Parse(parts[1]);

                            // Check if the host exists
                            var isActive = await _proxyRepository.CheckHostExistAsync(host, port);
                            return new ProxyDto
                            {
                                Host = host,
                                Port = port,
                                IsActive = isActive
                            };
                        }
                        return null;
                    }).ToList();

                    var proxyResults = (await Task.WhenAll(proxyTasks)).Where(p => p != null).ToList();

                    var lstProxies = proxyResults.Where(p => p != null && !p.IsActive).ToList();
                    if (lstProxies.Count < 0)
                    {
                        throw new Exception("");
                    }
                    var dataInsert = ObjectMapper.Map<List<ProxyDto>, List<Proxies>>(lstProxies);
                    dataInsert.ForEach(proxy => proxy.ProxyType = input.ProxyType);

                    if (dataInsert.Count > 0)
                    {
                        await _proxyRepository.InsertManyAsync(dataInsert, autoSave: true);

                    }

                    return new PagedResultDto<ProxyDto>(lstProxies.Count, lstProxies);

                }
                catch (System.Exception)
                {
                    await uow.RollbackAsync();

                    throw;
                }
                finally
                {
                    await page.CloseAsync();
                    await browser.CloseAsync();
                    await uow.CompleteAsync();
                }

            }
        }
        catch (System.Exception)
        {
            await uow.RollbackAsync();
            throw;
        }
    }
    private List<string> ExtractProxies(List<string> inputList)
    {
        // Sử dụng regex để tìm kiếm địa chỉ IP và port
        var regex = new Regex(@"(\d{1,3}(?:\.\d{1,3}){3}):(\d{2,5})");
        var result = new List<string>();

        // Duyệt qua từng chuỗi trong danh sách
        foreach (var input in inputList)
        {
            var matches = regex.Matches(input);
            // Duyệt qua các kết quả và thêm vào danh sách
            foreach (Match match in matches)
            {
                result.Add($"{match.Groups[1].Value}:{match.Groups[2].Value}");
            }
        }

        return result;
    }
}
