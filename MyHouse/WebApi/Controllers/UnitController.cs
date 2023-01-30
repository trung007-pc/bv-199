using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Units;
using Contract;
using Contract.Identity.UserManager;
using Contract.Units;
using Contract.UnitTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/unit/")]
    [Authorize]
    public class UnitController :  ControllerBase, IUnitService
    {
        private readonly UnitService _unitService;
        public UnitController(UnitService unitService)
        {
            _unitService = unitService;
        }
        
      
        [HttpPost]
        public async Task<UnitDto> CreateAsync(CreateUpdateUnitDto input)
        {
           return  await _unitService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<UnitDto> UpdateAsync(CreateUpdateUnitDto input, Guid id)
        {
            return await _unitService.UpdateAsync(input,id);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _unitService.DeleteAsync(id);
        }
        
        [HttpPost]
        [Route("get-list-with-nav-properties")]
        [AllowAnonymous]
        public async Task<List<UnitWithNavPropertiesDto>> GetListWithNavPropertiesAsync(UnitFilter input)
        {
            return await _unitService.GetListWithNavPropertiesAsync(input);
        }


        [HttpGet]
        [Route("look-up-unit-types")]
        public async Task<List<UnitTypeDto>> LookUpUnitTypes()
        {
            return await _unitService.LookUpUnitTypes();
        }
    }
}