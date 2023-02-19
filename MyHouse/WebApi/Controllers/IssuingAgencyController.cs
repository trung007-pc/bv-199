using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.IssuingAgencys;
using Contract.IssuingAgencys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/issuing-agency/")]
    [Authorize]
    public class IssuingAgencyController : I_IssuingAgencyService
    {
        private IssuingAgencyService _issuingAgencyService;
        
        public IssuingAgencyController(IssuingAgencyService issuingAgencyService)
        {
            _issuingAgencyService = issuingAgencyService;
        }
        
        [HttpPost]
        public async Task<IssuingAgencyDto> CreateAsync(CreateUpdateIssuingAgencyDto input)
        {
            return await _issuingAgencyService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IssuingAgencyDto> UpdateAsync(CreateUpdateIssuingAgencyDto input, Guid id)
        {
            return  await _issuingAgencyService.UpdateAsync(input,id);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _issuingAgencyService.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<List<IssuingAgencyDto>> GetListAsync()
        {
            return await _issuingAgencyService.GetListAsync();
        }
    }
}