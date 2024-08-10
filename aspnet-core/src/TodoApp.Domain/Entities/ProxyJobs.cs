using System;
using System.ComponentModel.DataAnnotations;
using Acme.BookStore.Books;
using Volo.Abp.Domain.Entities;

namespace TodoApp

{
    public class ProxyJobs : BasicAggregateRoot<Guid>, ModificationAuditing
    {

        public ProxyJobs()
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.UtcNow; // Ensure CreationTime is set
        }
        public string? MainUrl { get; set; }

        public string? Url { get; set; }
        public bool IsActive { get; set; }
        public bool IsRawUrl { get; set; }

        public ProxyType ProxyType { get; set; }
        public FileType FileType { get; set; }
        public int? TimeRun { get; set; }

        public int? Version { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string LastModificationBy { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreationBy { get; set; }



    }
}