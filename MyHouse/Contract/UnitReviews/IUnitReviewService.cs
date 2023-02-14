using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.UnitReviewDetails;
using Contract.Units;

namespace Contract.UnitReviews
{
    public interface IUnitReviewService
    {
        Task<List<UnitReviewDto>> GetListWithCalculatingAverageAsync(UnitReviewFilter input);
        Task<UnitReviewDto> CreateAsync(CreateUpdateUnitReviewDto input);
        Task<UnitReviewDto> UpdateAsync(CreateUpdateUnitReviewDto input,Guid Id);
        Task<UnitReviewDto> CreateReviewWithDetailsAsync(List<CreateUpdateUnitReviewDetailDto> inputs);
        Task DeleteAsync(Guid reviewId);



    }
}