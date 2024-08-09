using System;
using System.Collections.Generic;
using Acme.BookStore.Books;
using Volo.Abp.Domain.Entities;

namespace TodoApp
{
    public class TikiCategory : BasicAggregateRoot<Guid>, ModificationAuditing
    {
        public TikiCategory()
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.UtcNow; // Ensure CreationTime is set
            LastModificationTime = DateTime.UtcNow;

        }
        public string? Name { get; set; } = "";
        public string? Url { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public DateTime? LastModificationTime { get; set; }
        public string? LastModificationBy { get; set; }= "system";
        public DateTime CreationTime { get; set; }
        public string? CreationBy { get; set; }= "system";
        public ICollection<TikiProductLink> tikiProductLinks { get; set; }
    }
}