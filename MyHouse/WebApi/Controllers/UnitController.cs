using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Units;
using Contract;
using Contract.Identity.UserManager;
using Contract.Units;
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

        [HttpGet]
        public async Task<List<UnitDto>> GetListAsync()
        {
            return await _unitService.GetListAsync();
        }

        [HttpPost]
        [Route("get-list-by-filter")]
        [AllowAnonymous]
        public async Task<List<UnitDto>> GetListAsync(UnitFilter input)
        {
            return await _unitService.GetListAsync(input);
        }
    }
}