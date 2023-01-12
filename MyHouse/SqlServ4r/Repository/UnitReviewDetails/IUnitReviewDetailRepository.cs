using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.UnitReviewDetails;

namespace SqlServ4r.Repository.UnitReviewDetails
{
    public interface IUnitReviewDetailRepository
    {
        Task<List<UnitReviewDetail>> GetListWithNavigationPropertiesAsync(Guid reviewId);

    }
}