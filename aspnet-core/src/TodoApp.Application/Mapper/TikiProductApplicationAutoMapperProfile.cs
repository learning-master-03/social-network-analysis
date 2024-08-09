using Acme.BookStore.Books;
using AutoMapper;
using TodoApp;

namespace Acme.BookStore;

public class TikiProductApplicationAutoMapperProfile : Profile
{
    public TikiProductApplicationAutoMapperProfile()
    {
        CreateMap<TikiProductDto, TikiProduct>()
        .ReverseMap();

        CreateMap<TikiProductImageDto, TikiProductImage>()
        .ReverseMap();
          CreateMap<TikiProductImageDto, TikiProductImage>()
        .ReverseMap();
    }
}
