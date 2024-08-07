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
        }

        public int No { get; set; }

        public string ImageUrl { get; set; } = "";
        public bool IsActive { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public  string LastModificationBy { get; set; }
        public DateTime CreationTime { get; set; }
        public  string CreationBy { get; set; }
    }
}