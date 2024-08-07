using System;
using TodoApp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Books;

public class CategoryAppService :
    CrudAppService<
        TikiCategory, //The Book entity
        TikiCategoryDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateTikiCategoryDto>, //Used to create/update a book
    ICategoryAppService //implement the IBookAppService
{
    public CategoryAppService(IRepository<TikiCategory, Guid> repository)
        : base(repository)
    {

    }
}
