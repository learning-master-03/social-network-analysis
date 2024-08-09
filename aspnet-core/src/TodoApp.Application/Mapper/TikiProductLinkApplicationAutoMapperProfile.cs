using Acme.BookStore.Books;
using AutoMapper;
using TodoApp;

namespace Acme.BookStore;

public class TikiProductLinkApplicationAutoMapperProfile : Profile
{
    public TikiProductLinkApplicationAutoMapperProfile()
    {
        CreateMap<TikiProductLinkDto, TikiProductLink>()
        .ReverseMap();

        CreateMap<TikiCategoryDto, TikiCategory>()
        .ReverseMap();
    }
}
