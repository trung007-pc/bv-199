using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.WorkSchedules
{
    public interface IWorkScheduleService
    {
        Task<WorkScheduleDto> CreateAsync(CreateUpdateWorkScheduleDto input);
        Task<WorkScheduleDto> UpdateAsync(CreateUpdateWorkScheduleDto input,Guid id);
        Task DeleteAsync(Guid id);
        Task<List<WorkScheduleDto>> GetListAsync();
    }
}