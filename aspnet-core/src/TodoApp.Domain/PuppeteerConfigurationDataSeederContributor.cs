using System;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore
{
    public class PuppeteerConfigurationDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<PuppeteerConfiguration, Guid> _puppeteerConfigurationRepository;

        public PuppeteerConfigurationDataSeederContributor(IRepository<PuppeteerConfiguration, Guid> puppeteerConfigurationRepository)
        {
            _puppeteerConfigurationRepository = puppeteerConfigurationRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // Kiểm tra xem đã có dữ liệu mẫu chưa
            if (await _puppeteerConfigurationRepository.GetCountAsync() > 0)
            {
                return; // Dữ liệu đã tồn tại, không cần thêm nữa
            }

            // Tạo dữ liệu mẫu
            var configurations = new[]
            {
                new PuppeteerConfiguration
                {
                    Id = new Guid(), // Định danh duy nhất (có thể là 0 nếu không cần thiết)
                    Headless = false,
                    Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" },
                    ViewportWidth = 1366,
                    ViewportHeight = 768,
                    ExecutablePath = "",
                    SlowMo = 100,
                    Timeout = 30000,
                    UserDataDir = "",
                    IgnoreHTTPSErrors = true,
                    Devtools = false,
                    IgnoreDefaultArgs = false,
                    IgnoredDefaultArgs = Array.Empty<string>()
                }
            };

            // Thêm dữ liệu vào cơ sở dữ liệu
            foreach (var config in configurations)
            {
                await _puppeteerConfigurationRepository.InsertAsync(config);
            }
        }
    }
}
