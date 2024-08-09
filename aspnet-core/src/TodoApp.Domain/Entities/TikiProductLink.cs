using System;
using System.ComponentModel.DataAnnotations;
using Acme.BookStore.Books;
using Volo.Abp.Domain.Entities;

namespace TodoApp

{
    public class TikiProductLink : BasicAggregateRoot<Guid>, ModificationAuditing
    {

        public TikiProductLink()
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.UtcNow; // Ensure CreationTime is set
            LastModificationTime = DateTime.UtcNow;

        }

        public string? Url { get; set; }
        public bool IsGoTo { get; set; }
        public int Version { get; set; }
        public Guid? TikiCategoryId { get; set; } // Nullable foreign key

        public DateTime? LastModificationTime { get; set; }
        public string? LastModificationBy { get; set; }= "system";
        public DateTime CreationTime { get; set; }
        public string? CreationBy { get; set; }= "system";
    }
}