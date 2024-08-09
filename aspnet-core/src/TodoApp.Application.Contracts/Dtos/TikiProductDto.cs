using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Books;

public class TikiProductDto
{
    public TikiProductDto()
    {
        Id = Guid.NewGuid();
        TikiProductImages = new List<TikiProductImageDto>();
        TikiReviews = new List<TikiReviewDto>();
    }

    public Guid Id { get; set; }
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
    public List<TikiProductImageDto> TikiProductImages { get; set; }
    public List<TikiReviewDto> TikiReviews { get; set; }

}
