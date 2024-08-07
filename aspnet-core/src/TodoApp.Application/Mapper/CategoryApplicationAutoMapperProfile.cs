using Acme.BookStore.Books;
using AutoMapper;
using TodoApp;

namespace Acme.BookStore;

public class CategoryApplicationAutoMapperProfile : Profile
{
    public CategoryApplicationAutoMapperProfile()
    {
        CreateMap<TikiCategory, TikiCategoryDto>();
        CreateMap<CreateUpdateTikiCategoryDto, TikiCategory>();
    }
}
