using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Books;

public class CrawlProxyDto
{
    public Guid? Id { get; set; }
    public string? Url { get; set; }
    public bool IsRawUrl { get; set; }

    public ProxyType ProxyType { get; set; }
    public FileType FileType { get; set; }
}
