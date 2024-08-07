using System;
using Acme.BookStore.Books;
using Volo.Abp.Domain.Entities;

namespace TodoApp
{
    public class TikiProductImage : BasicAggregateRoot<Guid>, ModificationAuditing
    {
        public TikiProductImage()
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.UtcNow; // Ensure CreationTime is set
            LastModificationTime = DateTime.UtcNow;

        }

        public int No { get; set; }

        public string? ImageUrl { get; set; } = "";
        public bool IsActive { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string? LastModificationBy { get; set; }= "system";
        public DateTime CreationTime { get; set; }
        public string? CreationBy { get; set; }= "system";
        public TikiProduct tikiProduct { get; set; }
    }
}