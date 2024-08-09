using Acme.BookStore.Books;
using AutoMapper;
using TodoApp;

namespace Acme.BookStore;

public class TikiPreviewApplicationAutoMapperProfile : Profile
{
    public TikiPreviewApplicationAutoMapperProfile()
    {
        CreateMap<TikiReviewDto, TikiReview>()
        .ReverseMap();

    }
}
