using System;

namespace Contract.UnitReviewDetails
{
    public class UnitReviewDetailDto
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public Guid UnitReviewId { get; set; }
        public Guid UnitId { get; set; }
        public string UnitName { get; set;}
        public int Rate { get; set; }
        
    }



    
}