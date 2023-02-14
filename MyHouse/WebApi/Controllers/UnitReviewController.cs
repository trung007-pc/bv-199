using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UnitReviews;
using Contract;
using Contract.UnitReviewDetails;
using Contract.UnitReviews;
using Contract.Units;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/unit-review/")]
    [Authorize]
    public class UnitReviewController : IUnitReviewService
    {
        public readonly UnitReviewService UnitReviewService;
        
        public UnitReviewController(UnitReviewService unitReviewService)
        {
            UnitReviewService = unitReviewService;
        }
        
        [HttpPost]
        [Route("get-list-with-navigation")]
        public async Task<List<UnitReviewDto>> GetListWithCalculatingAverageAsync(UnitReviewFilter input)
        {
           return await UnitReviewService.GetListWithCalculatingAverageAsync(input);
        }
        

        [HttpPost]
        public async Task<UnitReviewDto> CreateAsync(CreateUpdateUnitReviewDto input)
        {
            return await UnitReviewService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{Id}")]
        [AllowAnonymous]
        public async Task<UnitReviewDto> UpdateAsync(CreateUpdateUnitReviewDto input, Guid Id)
        {
            return await UnitReviewService.UpdateAsync(input, Id);
        }

        [HttpPost]
        [Route("create-review-details")]
        [AllowAnonymous]
        public async Task<UnitReviewDto> CreateReviewWithDetailsAsync(List<CreateUpdateUnitReviewDetailDto> inputs)
        {
            return await UnitReviewService.CreateReviewWithDetailsAsync(inputs);
        }


        [HttpDelete]
        [Route("reviewId={reviewId}")]
        public async Task DeleteAsync(Guid reviewId)
        {
             await UnitReviewService.DeleteAsync(reviewId);
        }

        
        
    }
    
    
}