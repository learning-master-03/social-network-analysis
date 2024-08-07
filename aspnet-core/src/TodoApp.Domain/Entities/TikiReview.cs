using System;
using System.ComponentModel.DataAnnotations;
using Acme.BookStore.Books;
using Volo.Abp.Domain.Entities;

namespace TodoApp

{
    public class TikiReview : BasicAggregateRoot<Guid>, ModificationAuditing
    {

        public TikiReview()
        {
            Id = Guid.NewGuid();
        }

        public string UserName { get; set; }
        public string UserDate { get; set; }
        public string UserInfo { get; set; }
        public string ReviewTitle { get; set; }
        public string Rating { get; set; }
        public string RatingAttribute { get; set; }

        public string ReviewContent { get; set; }
        public string CreatedDate { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public  string LastModificationBy { get; set; }
        public DateTime CreationTime { get; set; }
        public  string CreationBy { get; set; }

    }
}