using System.Collections.Generic;
using Domain.UnitReviewDetails;
using Domain.Units;

namespace Contract.Units
{
    public class UnitWithNavProperties
    {
        public Unit Unit { get; set; }
        
        public IEnumerable<UnitReviewDetail> UnitReviewDetails { get; set;}

    }
    
    
    
    public class UnitsWithNavProperties
    {
        public Unit Unit { get; set; }
        
        public UnitReviewDetail UnitReviewDetail { get; set;}

    }
    
}