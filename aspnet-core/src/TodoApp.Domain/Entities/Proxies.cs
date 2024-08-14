using System;
using System.ComponentModel.DataAnnotations;
using Acme.BookStore.Books;
using Volo.Abp.Domain.Entities;

namespace TodoApp

{
    public class Proxies : BasicAggregateRoot<Guid>, ModificationAuditing
    {

        public Proxies()
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.UtcNow; // Ensure CreationTime is set
        }
        public string? Host { get; set; }
        public int? Port { get; set; }
        public bool IsActive { get; set; }
        public bool IsCanUsed { get; set; }

        public string? Country { get; set; }
        public ProxyType ProxyType { get; set; }
        public int? Latency { get; set; }
        public int? Version { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string LastModificationBy { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreationBy { get; set; }



    }
}