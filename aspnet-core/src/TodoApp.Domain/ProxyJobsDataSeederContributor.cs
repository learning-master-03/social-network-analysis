using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using TodoApp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Acme.BookStore
{
    public class ProxyJobsDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<ProxyJobs, Guid> _repository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public ProxyJobsDataSeederContributor(IRepository<ProxyJobs, Guid> repository, IUnitOfWorkManager unitOfWorkManager)
        {
            _repository = repository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            var uow = _unitOfWorkManager.Begin(requiresNew: true, isTransactional: true);

            try
            {
                // Kiểm tra xem đã có dữ liệu mẫu chưa
                if (await _repository.GetCountAsync() > 0)
                {
                    return; // Dữ liệu đã tồn tại, không cần thêm nữa
                }
                await _repository.InsertAsync(
                              new ProxyJobs
                              {
                                  MainUrl = "https://github.com/Zaeem20/FREE_PROXIES_LIST",
                                  Url = "https://github.com/Zaeem20/FREE_PROXIES_LIST/blob/master/http.txt",
                                  IsActive = true,
                                  ProxyType = ProxyType.HTTP,
                                  TimeRun = 600000,
                                  Version = 0,
                                  LastModificationBy = "system",
                                  CreationBy = "system",
                                  LastModificationTime = DateTime.UtcNow,
                                  CreationTime = DateTime.UtcNow,
                                  FileType = FileType.TXT,
                                  IsRawUrl = false,

                              },
                              autoSave: true
                          );

                await _repository.InsertAsync(
                           new ProxyJobs
                           {
                               MainUrl = "https://github.com/Zaeem20/FREE_PROXIES_LIST",
                               Url = "https://github.com/Zaeem20/FREE_PROXIES_LIST/blob/master/https.txt",
                               IsActive = true,
                               ProxyType = ProxyType.HTTPS,
                               TimeRun = 600000,
                               Version = 0,
                               LastModificationBy = "system",
                               CreationBy = "system",
                               LastModificationTime = DateTime.UtcNow,
                               CreationTime = DateTime.UtcNow,
                               FileType = FileType.TXT,
                               IsRawUrl = false
                           },
                           autoSave: true
                       );
                await _repository.InsertAsync(
 new ProxyJobs
 {
     MainUrl = "https://github.com/Zaeem20/FREE_PROXIES_LIST",
     Url = "https://github.com/Zaeem20/FREE_PROXIES_LIST/blob/master/socks4.txt",
     IsActive = true,
     ProxyType = ProxyType.SOCKS4,
     TimeRun = 600000,
     Version = 0,
     LastModificationBy = "system",
     CreationBy = "system",
     LastModificationTime = DateTime.UtcNow,
     CreationTime = DateTime.UtcNow,
     FileType = FileType.TXT,
     IsRawUrl = false
 },
 autoSave: true
);
                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/Zaeem20/FREE_PROXIES_LIST",
    Url = "https://github.com/Zaeem20/FREE_PROXIES_LIST/blob/master/socks5.txt",
    IsActive = true,
    ProxyType = ProxyType.SOCKS5,
    TimeRun = 600000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = false
},
autoSave: true
);
                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/im-razvan/proxy_list",
    Url = "https://github.com/im-razvan/proxy_list/blob/main/http.txt",
    IsActive = true,
    ProxyType = ProxyType.HTTP,
    TimeRun = 900000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = false
},
autoSave: true
);
                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/im-razvan/proxy_list",
    Url = "https://github.com/im-razvan/proxy_list/blob/main/socks5.txt",
    IsActive = true,
    ProxyType = ProxyType.SOCKS5,
    TimeRun = 900000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = false
},
autoSave: true
);
                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/claude89757/free_https_proxies",
    Url = "https://github.com/claude89757/free_https_proxies/blob/main/free_https_proxies.txt",
    IsActive = true,
    ProxyType = ProxyType.HTTPS,
    TimeRun = 900000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = false
},
autoSave: true
);

                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/BreakingTechFr/Proxy_Free/tree/main/proxies",
    Url = "https://github.com/BreakingTechFr/Proxy_Free/blob/main/proxies/http.txt",
    IsActive = true,
    ProxyType = ProxyType.HTTP,
    TimeRun = 600000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = false
},
autoSave: true
);

                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/BreakingTechFr/Proxy_Free/tree/main/proxies",
    Url = "https://github.com/BreakingTechFr/Proxy_Free/blob/main/proxies/socks4.txt",
    IsActive = true,
    ProxyType = ProxyType.SOCKS4,
    TimeRun = 600000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = false
},
autoSave: true
);

                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/BreakingTechFr/Proxy_Free/tree/main/proxies",
    Url = "https://github.com/BreakingTechFr/Proxy_Free/blob/main/proxies/socks5.txt",
    IsActive = true,
    ProxyType = ProxyType.SOCKS5,
    TimeRun = 600000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = false
},
autoSave: true
);
                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/Anonym0usWork1221/Free-Proxies/tree/main/proxy_files",
    Url = "https://github.com/Anonym0usWork1221/Free-Proxies/blob/main/proxy_files/http_proxies.txt",
    IsActive = true,
    ProxyType = ProxyType.HTTP,
    TimeRun = 900000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = false
},
autoSave: true
);

                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/Anonym0usWork1221/Free-Proxies/tree/main/proxy_files",
    Url = "https://github.com/Anonym0usWork1221/Free-Proxies/blob/main/proxy_files/https_proxies.txt",
    IsActive = true,
    ProxyType = ProxyType.HTTPS,
    TimeRun = 900000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = false
},
autoSave: true
);
                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/Anonym0usWork1221/Free-Proxies/tree/main/proxy_files",
    Url = "https://github.com/Anonym0usWork1221/Free-Proxies/blob/main/proxy_files/socks4_proxies.txt",
    IsActive = true,
    ProxyType = ProxyType.SOCKS4,
    TimeRun = 900000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = false
},
autoSave: true
);
                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/Anonym0usWork1221/Free-Proxies/tree/main/proxy_files",
    Url = "https://github.com/Anonym0usWork1221/Free-Proxies/blob/main/proxy_files/socks5_proxies.txt",
    IsActive = true,
    ProxyType = ProxyType.SOCKS5,
    TimeRun = 900000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = false
},
autoSave: true
);
                await _repository.InsertAsync(
            new ProxyJobs
            {
                MainUrl = "https://github.com/officialputuid/KangProxy/tree/KangProxy/http",
                Url = "https://github.com/officialputuid/KangProxy/blob/KangProxy/http/http.txt",
                IsActive = true,
                ProxyType = ProxyType.HTTP,
                TimeRun = 18000000,
                Version = 0,
                LastModificationBy = "system",
                CreationBy = "system",
                LastModificationTime = DateTime.UtcNow,
                CreationTime = DateTime.UtcNow,
                FileType = FileType.TXT,
                IsRawUrl = false
            },
            autoSave: true
            );

                await _repository.InsertAsync(
            new ProxyJobs
            {
                MainUrl = "https://github.com/officialputuid/KangProxy/tree/KangProxy/https",
                Url = "https://github.com/officialputuid/KangProxy/blob/KangProxy/https/https.txt",
                IsActive = true,
                ProxyType = ProxyType.HTTPS,
                TimeRun = 18000000,
                Version = 0,
                LastModificationBy = "system",
                CreationBy = "system",
                LastModificationTime = DateTime.UtcNow,
                CreationTime = DateTime.UtcNow,
                FileType = FileType.TXT,
                IsRawUrl = false
            },
            autoSave: true
            );
                await _repository.InsertAsync(
            new ProxyJobs
            {
                MainUrl = "https://github.com/officialputuid/KangProxy/tree/KangProxy/socks4",
                Url = "https://github.com/officialputuid/KangProxy/blob/KangProxy/socks4/socks4.txt",
                IsActive = true,
                ProxyType = ProxyType.SOCKS4,
                TimeRun = 18000000,
                Version = 0,
                LastModificationBy = "system",
                CreationBy = "system",
                LastModificationTime = DateTime.UtcNow,
                CreationTime = DateTime.UtcNow,
                FileType = FileType.TXT,
                IsRawUrl = false
            },
            autoSave: true
            );
                await _repository.InsertAsync(
            new ProxyJobs
            {
                MainUrl = "https://github.com/officialputuid/KangProxy/tree/KangProxy/socks5",
                Url = "https://github.com/officialputuid/KangProxy/blob/KangProxy/socks5/socks5.txt",
                IsActive = true,
                ProxyType = ProxyType.SOCKS5,
                TimeRun = 18000000,
                Version = 0,
                LastModificationBy = "system",
                CreationBy = "system",
                LastModificationTime = DateTime.UtcNow,
                CreationTime = DateTime.UtcNow,
                FileType = FileType.TXT,
                IsRawUrl = false
            },
            autoSave: true
            );
                await _repository.InsertAsync(
            new ProxyJobs
            {
                MainUrl = "https://github.com/0x1337fy/fresh-proxy-list?tab=readme-ov-file",
                Url = "https://raw.githubusercontent.com/0x1337fy/fresh-proxy-list/archive/storage/classic/http.txt",
                IsActive = true,
                ProxyType = ProxyType.HTTP,
                TimeRun = 500000,
                Version = 0,
                LastModificationBy = "system",
                CreationBy = "system",
                LastModificationTime = DateTime.UtcNow,
                CreationTime = DateTime.UtcNow,
                FileType = FileType.TXT,
                IsRawUrl = true
            },
            autoSave: true
            );
                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/0x1337fy/fresh-proxy-list?tab=readme-ov-file",
    Url = "https://raw.githubusercontent.com/0x1337fy/fresh-proxy-list/archive/storage/classic/https.txt",
    IsActive = true,
    ProxyType = ProxyType.HTTPS,
    TimeRun = 500000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = true
},
autoSave: true
);
                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/0x1337fy/fresh-proxy-list?tab=readme-ov-file",
    Url = "https://raw.githubusercontent.com/0x1337fy/fresh-proxy-list/archive/storage/classic/socks4.txt",
    IsActive = true,
    ProxyType = ProxyType.SOCKS4,
    TimeRun = 500000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = true
},
autoSave: true
);
                await _repository.InsertAsync(
new ProxyJobs
{
    MainUrl = "https://github.com/0x1337fy/fresh-proxy-list?tab=readme-ov-file",
    Url = "https://raw.githubusercontent.com/0x1337fy/fresh-proxy-list/archive/storage/classic/socks5.txt",
    IsActive = true,
    ProxyType = ProxyType.SOCKS5,
    TimeRun = 500000,
    Version = 0,
    LastModificationBy = "system",
    CreationBy = "system",
    LastModificationTime = DateTime.UtcNow,
    CreationTime = DateTime.UtcNow,
    FileType = FileType.TXT,
    IsRawUrl = true
},
autoSave: true
);


                await uow.CompleteAsync();

            }
            catch (System.Exception)
            {
                await uow.RollbackAsync();
                throw;
            }
        }

    }

}
