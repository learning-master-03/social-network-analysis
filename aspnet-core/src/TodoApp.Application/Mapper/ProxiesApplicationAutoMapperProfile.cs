using System;
using Acme.BookStore.Books;
using AutoMapper;
using TodoApp;

namespace Acme.BookStore;

public class ProxiesApplicationAutoMapperProfile : Profile
{
    public ProxiesApplicationAutoMapperProfile()
    {
        CreateMap<ProxyDto, Proxies>()
        .ForMember(d => d.Host , s => s.MapFrom(s => s.Host))
        .ForMember(d => d.Port , s => s.MapFrom(s => s.Port))
        .ForMember(d => d.Latency, s => s.MapFrom(s => 0))
        .ForMember(d => d.Version, s => s.MapFrom(s => 0))
        .ForMember(d => d.Country, s => s.MapFrom(s => ""))
        .ForMember(d => d.CreationBy, s => s.MapFrom(s => "system"))
        .ForMember(d => d.LastModificationBy, s => s.MapFrom(s => "system"))
        .ForMember(d => d.CreationTime, s => s.MapFrom(s => DateTime.UtcNow))
        .ForMember(d => d.LastModificationTime, s => s.MapFrom(s => DateTime.UtcNow))
        .ForMember(d => d.ProxyType, s => s.Ignore())


        ;

    }
}
