using System;
using System.Collections.Generic;
using Domain.UnitReviewDetails;

namespace Domain.UnitReviews
{
    public class UnitReview
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Note { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        
        
        
        //media
        public string? FileName { get; set; }
        public string? Path { get; set;}
        public string? ImageUrl { get; set;}
        
        
        public IList<UnitReviewDetail> UnitReviewDetails { get; set; }

    }
}