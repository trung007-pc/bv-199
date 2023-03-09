using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using Contract.WorkSchedules;
using Core.Const;
using Core.Exceptions;
using Domain.WorkSchedules;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.Repository.WorkSchedules;
using Volo.Abp.DependencyInjection;

namespace Application.WorkSchedules
{
    public class WorkScheduleService : ServiceBase,IWorkScheduleService,ITransientDependency
    {
        
        private readonly WorkScheduleRepository _workScheduleRepository;
        public WorkScheduleService(WorkScheduleRepository workScheduleRepository)
        {
            _workScheduleRepository = workScheduleRepository;
        }
        
        public async Task<List<WorkScheduleDto>> GetListAsync()
        {
            var workSchedules = await _workScheduleRepository.GetListAsync();
            var workSchedulesDto = ObjectMapper.Map<List<WorkSchedule>, List<WorkScheduleDto>>(workSchedules);
            return workSchedulesDto;
        }
        
        public async Task<WorkScheduleDto> CreateAsync(CreateUpdateWorkScheduleDto input)
        {
            input.Name = input.Name.Trim();
            var workSchedule = ObjectMapper.Map<CreateUpdateWorkScheduleDto, WorkSchedule>(input);
            await _workScheduleRepository.AddAsync(workSchedule);
            return ObjectMapper.Map<WorkSchedule, WorkScheduleDto>(workSchedule);
        }

        public async Task<WorkScheduleDto> UpdateAsync(CreateUpdateWorkScheduleDto input, Guid id)
        {
            input.Name = input.Name.Trim();
            var item = await _workScheduleRepository.FirstOrDefaultAsync(x => x.Id == id);
            
            if(item is null)  throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            
            var workSchedule = ObjectMapper.Map(input,item);
            _workScheduleRepository.Update(workSchedule);
            
            return ObjectMapper.Map<WorkSchedule, WorkScheduleDto>(workSchedule);
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var item = await _workScheduleRepository.FirstOrDefaultAsync(x => x.Id == id);
            if(item is null)  throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            _workScheduleRepository.Remove(item);
        }

    }
}