using System;

namespace Contract.UnitReviewDetails
{
    public class CreateUpdateUnitReviewDetailDto
    {
        public Guid? UnitReviewId { get; set; }
        public Guid? UnitId { get; set; }
        public int Rate { get; set; }
    }
}