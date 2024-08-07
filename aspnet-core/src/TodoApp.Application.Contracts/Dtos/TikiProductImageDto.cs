using System;
using Acme.BookStore.Books;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.Books;

public class TikiProductImageDto
{
    public TikiProductImageDto()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public int No { get; set; }

    public string ImageUrl { get; set; } = "";
    public bool IsActive { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public required string LastModificationBy { get; set; }
    public DateTime CreationTime { get; set; }
    public required string CreationBy { get; set; }
}
