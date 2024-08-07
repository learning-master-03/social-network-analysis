using System;
using Acme.BookStore.Books;
using Volo.Abp.Domain.Entities;

namespace TodoApp
{
    public class TikiCategory : BasicAggregateRoot<Guid>, ModificationAuditing
    {
        public TikiCategory()
        {
            Id = Guid.NewGuid();

        }
        public string Name { get; set; } = "";
        public string Url { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public DateTime? LastModificationTime { get; set; }
        public  string LastModificationBy { get; set; }
        public DateTime CreationTime { get; set; }
        public  string CreationBy { get; set; }

    }
}