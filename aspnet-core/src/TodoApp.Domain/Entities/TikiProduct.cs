using System;
using System.Collections.Generic;
using Acme.BookStore.Books;
using Volo.Abp.Domain.Entities;

namespace TodoApp
{
    public class TikiProduct : BasicAggregateRoot<Guid>, ModificationAuditing
    {
        public TikiProduct()
        {
            Id = Guid.NewGuid();

        }

        public string Name { get; set; } = "";
        public string BrandName { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public string PrimaryCategory { get; set; } = "";
        public string TotalRating { get; set; } = "";
        public string AverageRating { get; set; } = "";
        public string Sold { get; set; } = "";
        public string Price { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public string Url { get; set; } = "";
        public bool IsActive { get; set; } = false;
        public DateTime? LastModificationTime { get; set; }
        public required string LastModificationBy { get; set; }
        public DateTime CreationTime { get; set; }
        public required string CreationBy { get; set; }
        public ICollection<TikiProductImage> TikiProductImages { get; set; }
        public ICollection<TikiReview> TikiReviews { get; set; }


    }
}