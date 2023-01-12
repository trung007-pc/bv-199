using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.UnitReviewDetails;
using WebClient.RequestHttp;

namespace WebClient.Service.UnitReviewDetails
{
    public class UnitReviewDetailService : IUnitReviewDetailService
    {

        public UnitReviewDetailService()
        {
            
        }
        
        public async Task<UnitReviewDetailDto> CreateAsync(CreateUpdateUnitReviewDetailDto input)
        {
            return await RequestClient.PostAPIAsync<UnitReviewDetailDto>($"unit-review-detail",input);
        }
        
        public async Task<UnitReviewDetailDto> UpdateAsync(CreateUpdateUnitReviewDetailDto input, Guid id)
        {
            return await RequestClient.PutAPIAsync<UnitReviewDetailDto>($"unit-review-detail/{id}",input);
        }
        
        public async Task DeleteAsync(Guid id)
        {
             await RequestClient.DeleteAPIAsync<Task>($"unit-review-detail/{id}");
        }
        
        public async Task<List<UnitReviewDetailDto>> GetListAsync()
        {
            return await RequestClient.GetAPIAsync<List<UnitReviewDetailDto>>($"unit-review-detail");

        }
        
        public async Task<List<UnitReviewDetailDto>> GetDetailsByReviewIdAsync(Guid reviewId)
        {
            return await RequestClient.GetAPIAsync<List<UnitReviewDetailDto>>($"unit-review-detail/get-details/{reviewId}");
        }
    }
}