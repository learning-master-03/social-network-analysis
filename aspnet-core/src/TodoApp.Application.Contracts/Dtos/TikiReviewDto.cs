 using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Books;
 
 public class TikiReviewDto
    {
        public string UserName { get; set; }
        public string UserDate { get; set; }
        public string UserInfo { get; set; }
        public string ReviewTitle { get; set; }
        public string Rating { get; set; }
        public string RatingAttribute { get; set; }

        public string ReviewContent { get; set; }
        public string CreatedDate { get; set; }
    }
