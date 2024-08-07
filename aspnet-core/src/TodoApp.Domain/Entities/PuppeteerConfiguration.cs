using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Books
{
    public class PuppeteerConfiguration : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// Định danh duy nhất cho cấu hình Puppeteer.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Xác định liệu trình duyệt có chạy ở chế độ headless (không có giao diện người dùng) hay không.
        /// - Nếu là true, trình duyệt sẽ chạy không có giao diện người dùng.
        /// - Nếu là false, trình duyệt sẽ chạy với giao diện người dùng hiển thị.
        /// </summary>
        public bool Headless { get; set; }

        /// <summary>
        /// Danh sách các đối số bổ sung để truyền cho phiên bản trình duyệt.
        /// Các đối số này sẽ được thêm vào lệnh khởi động của trình duyệt.
        /// Ví dụ: "--no-sandbox", "--disable-gpu", etc.
        /// </summary>
        public string[] Args { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Chiều rộng của viewport khi trình duyệt khởi động.
        /// </summary>
        public int ViewportWidth { get; set; }

        /// <summary>
        /// Chiều cao của viewport khi trình duyệt khởi động.
        /// </summary>
        public int ViewportHeight { get; set; }

        /// <summary>
        /// Đường dẫn tới trình duyệt Chromium hoặc Chrome tùy chỉnh để sử dụng thay vì trình duyệt Chromium được đóng gói sẵn.
        /// Nếu là đường dẫn tương đối, nó sẽ được giải quyết tương đối với thư mục làm việc hiện tại.
        /// </summary>
        public string ExecutablePath { get; set; }

        /// <summary>
        /// Thay đổi tốc độ của các thao tác Puppeteer bằng cách thêm độ trễ cụ thể (tính bằng milliseconds).
        /// Có thể được sử dụng để dễ dàng theo dõi và kiểm tra các thao tác.
        /// </summary>
        public int SlowMo { get; set; }

        /// <summary>
        /// Thời gian tối đa (tính bằng milliseconds) để chờ trình duyệt khởi động trước khi từ chối yêu cầu.
        /// Nếu đặt là 0, sẽ không có thời gian chờ.
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// Đường dẫn đến thư mục dữ liệu người dùng. Thư mục này sẽ được sử dụng để lưu dữ liệu phiên làm việc của trình duyệt.
        /// Điều này có thể hữu ích khi bạn muốn giữ lại các phiên làm việc, cookies hoặc cài đặt người dùng.
        /// </summary>
        public string UserDataDir { get; set; }

        /// <summary>
        /// Xác định liệu có bỏ qua các lỗi HTTPS khi điều hướng hay không.
        /// - Nếu là true, các lỗi HTTPS sẽ bị bỏ qua.
        /// - Nếu là false, lỗi HTTPS sẽ được xử lý bình thường.
        /// </summary>
        public bool IgnoreHTTPSErrors { get; set; }

        /// <summary>
        /// Xác định liệu DevTools có được tự động mở cho mỗi tab hay không.
        /// - Nếu là true, DevTools sẽ mở cho mỗi tab và chế độ headless sẽ được đặt thành false.
        /// - Nếu là false, DevTools sẽ không mở.
        /// </summary>
        public bool Devtools { get; set; }

        /// <summary>
        /// Xác định liệu các đối số mặc định của Puppeteer có bị bỏ qua hay không.
        /// - Nếu là true, các đối số mặc định sẽ bị bỏ qua.
        /// - Nếu là false, các đối số mặc định sẽ được sử dụng.
        /// </summary>
        public bool IgnoreDefaultArgs { get; set; }

        /// <summary>
        /// Nếu IgnoreDefaultArgs được đặt thành true, danh sách này sẽ chứa các đối số mặc định của Puppeteer mà bạn muốn bỏ qua.
        /// Đây là một mảng chứa các đối số.
        /// </summary>
        public string[] IgnoredDefaultArgs { get; set; } = Array.Empty<string>();
    }
}
