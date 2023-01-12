using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.UnitReviewDetails;
using Contract.UnitReviews;
using Contract.Units;
using WebClient.RequestHttp;

namespace WebClient.Service.UnitReviews
{
    public class UnitReviewService : IUnitReviewService
    {
        
        
        public async Task<List<UnitDto>> LookUpPartsAsync()
        {
            return await RequestClient.GetAPIAsync<List<UnitDto>>($"unit-review/look-up-units");
        }

        public async Task<List<UnitReviewDto>> GetListWithCalculatingAverageAsync()
        {
            return await RequestClient.GetAPIAsync<List<UnitReviewDto>>($"unit-review/get-list-with-navigation");
        }

        public async Task<List<UnitReviewDetailDto>> GetDetailsAsync(Guid reviewId)
        {
            return await RequestClient.GetAPIAsync<List<UnitReviewDetailDto>>($"unit-review/get-list-with-navigation");
        }

        public async  Task<UnitReviewDto> CreateAsync(CreateUpdateUnitReviewDto input)
        {
            return await RequestClient.PostAPIAsync<UnitReviewDto>($"unit-review",input,false);
        }

        public async Task<UnitReviewDto> UpdateAsync(CreateUpdateUnitReviewDto input, Guid Id)
        {
            return await RequestClient.PutAPIAsync<UnitReviewDto>($"unit-review/{Id}",input);
        }

        public async Task<UnitReviewDto> CreateReviewWithDetailsAsync(List<CreateUpdateUnitReviewDetailDto> inputs)
        {
            return await RequestClient.PostAPIAsync<UnitReviewDto>($"unit-review/create-review-details",inputs);
        }
        
        public async Task DeleteAsync(Guid reviewId)
        {
             await RequestClient.DeleteAPIAsync<Task>($"unit-review/reviewId={reviewId}");
        }
        

    
    }
}