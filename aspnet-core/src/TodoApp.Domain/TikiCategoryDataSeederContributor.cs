using System;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using TodoApp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore
{
    public class TikiCategoryDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<TikiCategory, Guid> _repository;

        public TikiCategoryDataSeederContributor(IRepository<TikiCategory, Guid> repository)
        {
            _repository = repository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            // Kiểm tra xem đã có dữ liệu mẫu chưa
            if (await _repository.GetCountAsync() > 0)
            {
                return; // Dữ liệu đã tồn tại, không cần thêm nữa
            }
            // Danh sách URL
            var urls = new[]
            {
                "https://tiki.vn/nha-sach-tiki/c8322",
                "https://tiki.vn/nha-cua-doi-song/c1883",
                "https://tiki.vn/dien-thoai-may-tinh-bang/c1789",
                "https://tiki.vn/do-choi-me-be/c2549",
                "https://tiki.vn/thiet-bi-kts-phu-kien-so/c1815",
                "https://tiki.vn/dien-gia-dung/c1882",
                "https://tiki.vn/lam-dep-suc-khoe/c1520",
                "https://tiki.vn/o-to-xe-may-xe-dap/c8594",
                "https://tiki.vn/thoi-trang-nu/c931",
                "https://tiki.vn/bach-hoa-online/c4384",
                "https://tiki.vn/the-thao-da-ngoai/c1975",
                "https://tiki.vn/thoi-trang-nam/c915",
                "https://tiki.vn/cross-border-hang-quoc-te/c17166",
                "https://tiki.vn/laptop-may-vi-tinh-linh-kien/c1846",
                "https://tiki.vn/giay-dep-nam/c1686",
                "https://tiki.vn/dien-tu-dien-lanh/c4221",
                "https://tiki.vn/giay-dep-nu/c1703",
                "https://tiki.vn/may-anh/c1801",
                "https://tiki.vn/phu-kien-thoi-trang/c27498",
                "https://tiki.vn/ngon/c44792",
                "https://tiki.vn/dong-ho-va-trang-suc/c8371",
                "https://tiki.vn/balo-va-vali/c6000",
                "https://tiki.vn/voucher-dich-vu/c11312",
                "https://tiki.vn/tui-vi-nu/c976",
                "https://tiki.vn/tui-thoi-trang-nam/c27616",
                "https://tiki.vn/cham-soc-nha-cua/c15078"
            };
            // Tạo danh sách các đối tượng TikiCategory từ các URL
            var categories = urls.Select(url => new TikiCategory
            {
                Name = ExtractNameFromUrl(url), // Tạo tên từ URL (hoặc đặt tên cụ thể nếu cần)
                Url = url,
                IsActive = false,
                LastModificationBy = "system",
                CreationBy = "system",
                LastModificationTime = DateTime.UtcNow,
                CreationTime = DateTime.UtcNow
            }).ToArray();

            // Thêm dữ liệu vào cơ sở dữ liệu
            foreach (var config in categories)
            {
                await _repository.InsertAsync(config);
            }
        }
        // Phương thức trợ giúp để tạo tên từ URL (có thể tùy chỉnh theo nhu cầu)
        private string ExtractNameFromUrl(string url)
        {
            // Đây là một phương pháp đơn giản để lấy tên từ URL
            // Bạn có thể cần tinh chỉnh để phù hợp với cách bạn muốn đặt tên các danh mục
            var segments = url.Split('/');
            return segments.Last().Replace("-c", ""); // Lấy phần cuối cùng của URL và loại bỏ phần -c nếu có
        }
    }

}
