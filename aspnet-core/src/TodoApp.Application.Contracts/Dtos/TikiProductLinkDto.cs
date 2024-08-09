using System;
using System.ComponentModel.DataAnnotations;
using TodoApp;

namespace Acme.BookStore.Books;

public class TikiProductLinkDto
{
    
    public string? Url { get; set; }
    public bool IsGoTo { get; set; }
    public int Version { get; set; }
    public Guid? TikiCategoryId { get; set; } // Nullable foreign key

    public DateTime? LastModificationTime { get; set; }
    public string? LastModificationBy { get; set; }
    public DateTime CreationTime { get; set; }
    public string? CreationBy { get; set; }
}
