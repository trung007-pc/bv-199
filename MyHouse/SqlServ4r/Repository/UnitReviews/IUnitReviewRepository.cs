using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.UnitReviews;

namespace SqlServ4r.Repository.UnitReviews
{
    public interface IUnitReviewRepository
    {
        Task<List<UnitReview>> GetListWithNavigationPropertiesAsync();
    }
}