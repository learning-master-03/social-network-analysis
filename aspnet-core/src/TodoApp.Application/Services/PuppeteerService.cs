using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using CsvHelper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PuppeteerSharp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace TodoApp
{
    public class PuppeteerService : ApplicationService, IPuppeteerService
    {
        private readonly IRepository<TikiCategory, Guid> _tikiCategoryRepository;
        private readonly IRepository<PuppeteerConfiguration, Guid> _puppeteerConfigurationRepository;


        public PuppeteerService(IRepository<TikiCategory, Guid> tikiCategoryRepository,
                                IRepository<PuppeteerConfiguration, Guid> puppeteerConfigurationRepository)
        {
            _tikiCategoryRepository = tikiCategoryRepository;
            _puppeteerConfigurationRepository = puppeteerConfigurationRepository;
        }

        public async Task CrawlProductAsync(Guid categoryId)
        {
            try
            {
                var reviews = new List<TikiReviewDto>();
                var products = new List<TikiProductDto>();
                var category = await _tikiCategoryRepository.FirstAsync(x => x.IsActive == true && x.Id == categoryId);
                var configPuppeteer = await _puppeteerConfigurationRepository.FirstAsync();
                Logger.LogInformation($"Starting: CrawlProductAsync... {JsonConvert.SerializeObject(category)}");

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

                    await page.GoToAsync(category.Url);
                    await Task.Delay(millisecondsDelay: 3000);
                    // while (true)
                    // {
                    //     try
                    //     {
                    //         await page.ClickAsync(".styles__Button-sc-143954l-1");
                    //         // await page.WaitForTimeoutAsync(2000); // Đợi tải trang tiếp theo
                    //         await Task.Delay(millisecondsDelay: 3000);
                    //     }
                    //     catch
                    //     {
                    //         break; // Không tìm thấy nút tiếp theo
                    //     }
                    // }

                    var productLinks = await page.EvaluateExpressionAsync<string[]>("Array.from(document.querySelectorAll('a.product-item')).map(a => a.href)");

                    foreach (var link in productLinks.OrderBy(x => x))
                    {
                        await page.GoToAsync(link);

                        var tikiItem = new TikiProductDto
                        {
                            Id = new Guid(),
                            Name = await page.EvaluateExpressionAsync<string>("document.querySelector('main h1')?.innerText"),
                            BrandName = await page.EvaluateExpressionAsync<string>("document.querySelector('span.brand-and-author h6 a')?.innerText"),
                            CategoryName = await page.EvaluateExpressionAsync<string>("document.querySelector('a.breadcrumb-item[data-view-index=\"1\"] span')?.innerText"),
                            PrimaryCategory = await page.EvaluateExpressionAsync<string>("document.querySelector('a.breadcrumb-item[data-view-index=\"2\"] span')?.innerText"),
                            TotalRating = await page.EvaluateExpressionAsync<string>("document.querySelector('a.number[data-view-id=\"pdp_main_view_review\"]')?.innerText"),
                            AverageRating = await page.EvaluateExpressionAsync<string>("document.querySelector('div.styles__StyledReview-sc-1onuk2l-1.dRFsZg div')?.innerText"),
                            Sold = await page.EvaluateExpressionAsync<string>("document.querySelector('div[data-view-id=\"pdp_quantity_sold\"].styles__StyledQuantitySold-sc-1onuk2l-3.eWJdKv')?.innerText"),
                            Price = await page.EvaluateExpressionAsync<string>("document.querySelector('div.product-price__current-price')?.innerText"),
                            ImageUrl = await page.EvaluateExpressionAsync<string>("document.querySelector('img.styles__StyledImg-sc-p9s3t3-0')?.srcset || document.querySelector('source[type=\"image/webp\"]').srcset.split(',')[0].trim()"),
                            Url = link,
                            IsActive = true
                        };

                        var listImageDetail = await page.EvaluateExpressionAsync<string[]>("Array.from(document.querySelectorAll('a.style__ThumbnailItemStyled-sc-g98s1e-1 img')).map(img => img.srcset)");
                        tikiItem.TikiProductImages = listImageDetail.Select((x, index) => new TikiProductImageDto()
                        {
                            No = index + 1,
                            ImageUrl = x,
                            IsActive = true,
                            LastModificationBy = "system",
                            CreationBy = "system",
                        }).ToList();


                        bool hasNextPage;
                        do
                        {
                            try
                            {
                                // Đảm bảo nội dung được tải đầy đủ
                                await page.WaitForSelectorAsync(".review-comment");

                                // Extract review details from the page
                                var reviewsOnPage = await page.EvaluateFunctionAsync<List<TikiReviewDto>>(@"
                        () => {
                            const reviews = [];
                            document.querySelectorAll('.review-comment').forEach(review => {
                                const userName = review.querySelector('.review-comment__user-name')?.innerText || '';
                                const userDate = review.querySelector('.review-comment__user-date')?.innerText || '';
                                const userInfo = review.querySelector('.review-comment__user-info')?.innerText || '';
                                const reviewTitle = review.querySelector('.review-comment__title')?.innerText || '';
                                const rating = review.querySelector('.review-comment__rating')?.innerText || '';
                                let reviewContent = review.querySelector('.review-comment__content')?.innerText || '';
                                const ratingAttributes = review.querySelector('#customer-review-widget-id > div > div.style__StyledCustomerReviews-sc-1y8vww-0.gCaHEu.customer-reviews > div > div.style__StyledComment-sc-1y8vww-5.dpVjwc.review-comment > div:nth-child(2) > div.wrapper-rating-attribute > div > span')?.innerText || '';

                                const showMoreButton = review.querySelector('.review-comment__content .show-more-content');
                                if (showMoreButton) {
                                    showMoreButton.click();
                                    reviewContent = review.querySelector('.review-comment__content')?.innerText || '';
                                }

                                const createdDate = review.querySelector('.review-comment__created-date')?.innerText || '';

                                reviews.push({
                                    UserName: userName,
                                    UserDate: userDate,
                                    UserInfo: userInfo,
                                    ReviewTitle: reviewTitle,
                                    Rating: rating,
                                    RatingAttribute:ratingAttributes,
                                    ReviewContent: reviewContent,
                                    CreatedDate: createdDate
                                });
                            });
                            return reviews;
                        }
                    ");

                                reviews.AddRange(reviewsOnPage);

                                // Check if there's a "next" button and click it
                                // hasNextPage = await page.EvaluateExpressionAsync<bool>("document.querySelector('.customer-reviews__pagination .btn.next') !== null");
                                hasNextPage = false;
                                if (hasNextPage)
                                {
                                    await page.ClickAsync(".customer-reviews__pagination .btn.next");
                                    await Task.Delay(RandomDelay(3000, 5000)); // Thêm khoảng thời gian trễ ngẫu nhiên
                                }

                            }
                            catch (System.Exception)
                            {

                                break;
                            }


                        } while (hasNextPage);

                        tikiItem.TikiReviews = reviews.Select(x => new TikiReviewDto()
                        {
                            UserName = x.UserName,
                            UserDate = x.UserDate,
                            UserInfo = x.UserInfo,
                            ReviewTitle = x.ReviewTitle,
                            Rating = x.Rating,
                            RatingAttribute = x.RatingAttribute,
                            ReviewContent = x.ReviewContent,
                            CreatedDate = x.CreatedDate
                        }).ToList();
                        products.Add(tikiItem);
                        break;
                    }

                }


                await Task.CompletedTask;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task CreateAsync()
        {
            string url = "https://tiki.vn/sach-nuoi-duong-tam-hon-shichida-bo-cun-con-tron-bo-gom-6-quyen-truyen-tranh-cho-be-3-tuoi-p273983039.html?itm_campaign=CTP_YPD_TKA_PLA_UNK_ALL_UNK_UNK_UNK_UNK_X.296134_Y.1878454_Z.3967726_CN.Product-Ads-15%2F07%2F2024&itm_medium=CPC&itm_source=tiki-ads&spid=273983040";

            var reviews = new List<TikiReviewDto>();

            await new BrowserFetcher().DownloadAsync();

            var launchOptions = new LaunchOptions
            {
                Headless = false, // Chạy trình duyệt không ở chế độ headless để dễ dàng theo dõi
                Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" }
            };

            using (var browser = await Puppeteer.LaunchAsync(launchOptions))
            using (var page = await browser.NewPageAsync())
            {
                // Set User-Agent to mimic a real browser
                await page.SetUserAgentAsync("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

                // Set viewport size
                await page.SetViewportAsync(new ViewPortOptions { Width = 1366, Height = 768 });

                await page.GoToAsync(url, WaitUntilNavigation.Networkidle2); // Chờ đến khi không còn kết nối mạng nào hoạt động

                bool hasNextPage;
                do
                {
                    try
                    {
                        // Đảm bảo nội dung được tải đầy đủ
                        await page.WaitForSelectorAsync(".review-comment");

                        // Extract review details from the page
                        var reviewsOnPage = await page.EvaluateFunctionAsync<List<TikiReviewDto>>(@"
                        () => {
                            const reviews = [];
                            document.querySelectorAll('.review-comment').forEach(review => {
                                const userName = review.querySelector('.review-comment__user-name')?.innerText || '';
                                const userDate = review.querySelector('.review-comment__user-date')?.innerText || '';
                                const userInfo = review.querySelector('.review-comment__user-info')?.innerText || '';
                                const reviewTitle = review.querySelector('.review-comment__title')?.innerText || '';
                                const rating = review.querySelector('.review-comment__rating')?.innerText || '';
                                let reviewContent = review.querySelector('.review-comment__content')?.innerText || '';
                                const ratingAttributes = review.querySelector('#customer-review-widget-id > div > div.style__StyledCustomerReviews-sc-1y8vww-0.gCaHEu.customer-reviews > div > div.style__StyledComment-sc-1y8vww-5.dpVjwc.review-comment > div:nth-child(2) > div.wrapper-rating-attribute > div > span')?.innerText || '';

                                const showMoreButton = review.querySelector('.review-comment__content .show-more-content');
                                if (showMoreButton) {
                                    showMoreButton.click();
                                    reviewContent = review.querySelector('.review-comment__content')?.innerText || '';
                                }

                                const createdDate = review.querySelector('.review-comment__created-date')?.innerText || '';

                                reviews.push({
                                    UserName: userName,
                                    UserDate: userDate,
                                    UserInfo: userInfo,
                                    ReviewTitle: reviewTitle,
                                    Rating: rating,
                                    RatingAttribute:ratingAttributes,
                                    ReviewContent: reviewContent,
                                    CreatedDate: createdDate
                                });
                            });
                            return reviews;
                        }
                    ");

                        reviews.AddRange(reviewsOnPage);

                        // Check if there's a "next" button and click it
                        hasNextPage = await page.EvaluateExpressionAsync<bool>("document.querySelector('.customer-reviews__pagination .btn.next') !== null");

                        if (hasNextPage)
                        {
                            await page.ClickAsync(".customer-reviews__pagination .btn.next");
                            await Task.Delay(RandomDelay(3000, 5000)); // Thêm khoảng thời gian trễ ngẫu nhiên
                        }

                    }
                    catch (System.Exception)
                    {

                        break;
                    }


                } while (hasNextPage);

                // Write data to CSV
                using (var writer = new StreamWriter("/Volumes/huynt/huy/studies/source/abp-demo/aspnet-core/src/TodoApp.Application/Services/tiki_reviews.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(reviews);
                }
            }
            Task.CompletedTask.Wait();
        }


        public int RandomDelay(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }
        public async Task SaveToCSV<T>(List<T> datas, string _outputPath)
        {
            if (datas == null) throw new ArgumentNullException(nameof(datas));
            if (string.IsNullOrEmpty(_outputPath)) throw new ArgumentException("Invalid file path", nameof(_outputPath));

            EnsureDirectoryExists(_outputPath); // Tạo thư mục nếu chưa tồn tại

            try
            {
                using (var fileStream = new FileStream(_outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var writer = new StreamWriter(fileStream))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    await csv.WriteRecordsAsync(datas); // Đảm bảo sử dụng phương thức async
                }

                // Thông báo thành công nếu cần
                Console.WriteLine("Data successfully saved to CSV.");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và ghi log nếu cần
                Console.WriteLine($"An error occurred while saving data to CSV: {ex.Message}");
                throw; // Đẩy lỗi ra ngoài nếu cần thiết
            }
        }

        private void EnsureDirectoryExists(string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
