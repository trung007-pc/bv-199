using System;
using System.Collections.Generic;

namespace Contract.Dashboards
{
    public class PartReviewStatisticsDto
    {
        public List<DataItem> PartReviewItems { get; set; } = new List<DataItem>();
        public int TotalReview { get; set;} 
        public List<DataItem> DetailPartReviewItems { get; set; } =new List<DataItem>();
    }

    public class RatingStatistics
    {
        public int  Star_1 { get; set; }
        public int  Star_2 { get; set; }
        public int  Star_3 { get; set; }
        public int  Star_4 { get; set; }
        public int  Star_5 { get; set; }

    }
    
}