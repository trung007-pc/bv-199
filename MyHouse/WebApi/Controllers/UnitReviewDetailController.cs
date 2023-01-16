using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UnitReviewDetails;
using Contract;
using Contract.UnitReviewDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/unit-review-detail/")]
    [Authorize]
    public class UnitReviewDetailController :ControllerBase, IUnitReviewDetailService
    {
        private readonly UnitReviewDetailService _unitReviewDetailService;
        
        public UnitReviewDetailController(UnitReviewDetailService unitReviewDetailService)
        {
            _unitReviewDetailService = unitReviewDetailService;
        }
        
        
        [HttpPost]
        public async Task<UnitReviewDetailDto> CreateAsync(CreateUpdateUnitReviewDetailDto input)
        {
           return  await _unitReviewDetailService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<UnitReviewDetailDto> UpdateAsync(CreateUpdateUnitReviewDetailDto input, Guid id)
        {
            return  await _unitReviewDetailService.UpdateAsync(input,id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
             await _unitReviewDetailService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<UnitReviewDetailDto>> GetListAsync()
        {
            return await _unitReviewDetailService.GetListAsync();
        }

        [HttpGet]
        [Route("get-details/{reviewId}")]
        public async Task<List<UnitReviewDetailDto>> GetDetailsByReviewIdAsync(Guid reviewId)
        {
            return await _unitReviewDetailService.GetDetailsByReviewIdAsync(reviewId);
        }
        
        
    }
}