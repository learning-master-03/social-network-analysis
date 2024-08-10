using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Books;

public class CheckProxyDto 
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public ProxyType ProxyType{ get; set; }
}
