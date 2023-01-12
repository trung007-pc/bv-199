using System;
using System.Collections.Generic;

namespace Contract.Dashboards
{
    public class UnitReviewStatisticsDto
    {
        public List<DataItem> UnitReviewItems { get; set; } = new List<DataItem>();
        public int TotalReview { get; set;} 
        public List<DataItem> DetailUnitReviewItems { get; set; } =new List<DataItem>();
    }
    
    
}