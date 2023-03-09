using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.WorkSchedules;
using WebClient.RequestHttp;

namespace WebClient.Service.WorkSchedules
{
    public class WorkScheduleService : IWorkScheduleService
    {
        public async Task<List<WorkScheduleDto>> GetListAsync()
        {
            return await RequestClient.GetAPIAsync<List<WorkScheduleDto>>("work-schedule");
        }

        public async Task<WorkScheduleDto> CreateAsync(CreateUpdateWorkScheduleDto input)
        {
            return await RequestClient.PostAPIAsync<WorkScheduleDto>("work-schedule",input);
        }

        public async  Task<WorkScheduleDto> UpdateAsync(CreateUpdateWorkScheduleDto input, Guid id)
        {
            return await RequestClient.PutAPIAsync<WorkScheduleDto>($"work-schedule/{id}",input);
        }

        public async Task DeleteAsync(Guid id)
        {
            await RequestClient.DeleteAPIAsync<Task>($"work-schedule/{id}");
        }
    }
}