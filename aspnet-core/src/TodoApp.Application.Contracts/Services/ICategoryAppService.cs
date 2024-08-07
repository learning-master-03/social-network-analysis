using System;
using TodoApp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Books;

public interface ICategoryAppService :
    ICrudAppService< //Defines CRUD methods
        TikiCategoryDto, //Used to show books
        Guid, //Primary key of the book entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateTikiCategoryDto> //Used to create/update a book
{

}
