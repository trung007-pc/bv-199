using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.WorkSchedules;
using Contract.WorkSchedules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/work-schedule/")]
    [Authorize]
    public class WorkScheduleController : IWorkScheduleService
    {
        private readonly WorkScheduleService _workScheduleService;
        
        public WorkScheduleController(WorkScheduleService workScheduleService)
        {
            _workScheduleService = workScheduleService;
        }
        
        
        [HttpGet]
        public async Task<List<WorkScheduleDto>> GetListAsync()
        {
            return await _workScheduleService.GetListAsync();
        } 

        [HttpPost]
        public async Task<WorkScheduleDto> CreateAsync(CreateUpdateWorkScheduleDto input)
        {
            return await _workScheduleService.CreateAsync(input);

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<WorkScheduleDto> UpdateAsync(CreateUpdateWorkScheduleDto input, Guid id)
        {
            return await _workScheduleService.UpdateAsync(input,id);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _workScheduleService.DeleteAsync(id);
        }
    }
}