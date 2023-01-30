using System;

namespace Contract.UnitReviews
{
    public class UnitReviewDto
    {
        public Guid Id { get; set;}
        public string Note { get; set; }
        public int AveragePoint { get; set;}
        public DateTime CreationDate { get; set; } = DateTime.Now;
        
        
        public string? ImageUrl { get; set; } 
    }
}