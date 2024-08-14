using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TodoApp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Books;

public class InformationService : ApplicationService, IInformationService
{
    public InformationService() { }
    public async Task<ServerInfoDto> GetServerInfoAsync()
    {
        var serverInfo = new ServerInfoDto
        {
            HostName = Environment.MachineName,
            IpAddress = GetLocalIPAddress(),
            OperatingSystem = Environment.OSVersion.ToString(),
            DotNetVersion = Environment.Version.ToString()
        };

        // Lấy thông tin về Thread Pool
        ThreadPool.GetMinThreads(out int minWorkerThreads, out int minCompletionPortThreads);
        ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);
        ThreadPool.GetAvailableThreads(out int availableWorkerThreads, out int availableCompletionPortThreads);

        serverInfo.MinWorkerThreads = minWorkerThreads;
        serverInfo.MaxWorkerThreads = maxWorkerThreads;
        serverInfo.AvailableWorkerThreads = availableWorkerThreads;
        serverInfo.MinCompletionPortThreads = minCompletionPortThreads;
        serverInfo.MaxCompletionPortThreads = maxCompletionPortThreads;
        serverInfo.AvailableCompletionPortThreads = availableCompletionPortThreads;

        return await Task.FromResult(serverInfo);
    }

    private string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "No network adapters with an IPv4 address in the system!";
    }
}
