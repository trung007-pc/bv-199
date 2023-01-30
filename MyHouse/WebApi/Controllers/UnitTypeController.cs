using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UnitTypes;
using Contract.UnitTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/unit-type/")]
    [Authorize]
    public class UnitTypeController : IUnitTypeService
    {
        private readonly UnitTypeService _unitTypeService;
        
        public UnitTypeController(UnitTypeService unitTypeService)
        {
            _unitTypeService = unitTypeService;
        }
        
        
        [HttpGet]
        public async Task<List<UnitTypeDto>> GetListAsync()
        {
            return await _unitTypeService.GetListAsync();
        } 

        [HttpPost]
        public async Task<UnitTypeDto> CreateAsync(CreateUpdateUnitTypeDto input)
        {
            return await _unitTypeService.CreateAsync(input);

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<UnitTypeDto> UpdateAsync(CreateUpdateUnitTypeDto input, Guid id)
        {
            return await _unitTypeService.UpdateAsync(input,id);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
             await _unitTypeService.DeleteAsync(id);
        }
    }
}