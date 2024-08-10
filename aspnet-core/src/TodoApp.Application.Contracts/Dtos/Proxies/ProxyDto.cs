using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Books;

public class ProxyDto 
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public bool IsActive { get; set; }
}
