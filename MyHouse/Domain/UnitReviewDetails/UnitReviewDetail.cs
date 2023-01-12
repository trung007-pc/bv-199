using System;
using Domain.UnitReviews;
using Domain.Units;

namespace Domain.UnitReviewDetails
{
    public class UnitReviewDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UnitReviewId { get; set; }
        public Guid UnitId { get; set; }
        public int Rate { get; set; }

        public UnitReview UnitReview { get; set; }
        public Unit Unit { get; set; }
    }
}