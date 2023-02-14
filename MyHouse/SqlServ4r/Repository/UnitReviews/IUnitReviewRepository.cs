using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.UnitReviews;
using Domain.UnitReviews;

namespace SqlServ4r.Repository.UnitReviews
{
    public interface IUnitReviewRepository
    {
        Task<List<UnitReview>> GetListWithNavigationPropertiesAsync(UnitReviewFilter input);
    }
}