using System;
using Contract.Base;

namespace Contract.UnitReviews
{
    public class UnitReviewFilter : FilterBase
    {
        public DateTime? StartDay { get; set;}
        public DateTime? EndDay { get; set; }
    }
}