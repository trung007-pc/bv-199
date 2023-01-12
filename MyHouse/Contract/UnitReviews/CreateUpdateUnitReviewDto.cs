using System;

namespace Contract.UnitReviews
{
    public class CreateUpdateUnitReviewDto
    {
        public string? Note { get; set; } 
        public int Rating { get; set;}
        public Guid UnitId { get; set; }
        
        
        
        //media
        public string? FileName { get; set; }
        public string? Path { get; set;}
        public string? ImageUrl { get; set;}
        
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}