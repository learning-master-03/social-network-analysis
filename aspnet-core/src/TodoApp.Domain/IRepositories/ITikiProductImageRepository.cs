using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TodoApp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace Acme.BookStore.Books;

public interface ITikiProductImageRepository : IRepository<TikiProductImage, Guid>
{
}
