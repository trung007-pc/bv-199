using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.UnitReviewDetails
{
    public interface IUnitReviewDetailService
    {
        public Task<UnitReviewDetailDto> CreateAsync(CreateUpdateUnitReviewDetailDto input);
        public Task<UnitReviewDetailDto> UpdateAsync(CreateUpdateUnitReviewDetailDto input,Guid id);
        public Task DeleteAsync(Guid id);
        public Task<List<UnitReviewDetailDto>> GetListAsync();

        public Task<List<UnitReviewDetailDto>> GetDetailsByReviewIdAsync(Guid reviewId);
    }
}