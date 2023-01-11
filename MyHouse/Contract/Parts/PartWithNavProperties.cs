using System.Collections.Generic;
using Domain.PartReviewDetails;
using Domain.PartReviews;
using Domain.Parts;

namespace Contract.Parts
{
    public class PartWithNavProperties
    {
        public Part Part { get; set; }
        
        public IEnumerable<PartReviewDetail> PartReviewDetails { get; set;}

    }
    
    
    public class PartWithNavProperties1
    {
        public Part Part { get; set; }
        public IEnumerable<PartReviewDetail> PartReviewDetails { get; set;}
    }
}