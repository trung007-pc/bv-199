using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Identity.RoleManager;
using Contract.UnitTypes;
using WebClient.RequestHttp;

namespace WebClient.Service.UnitTypes
{
    public class UnitTypeService : IUnitTypeService
    {
        
        
        public async Task<List<UnitTypeDto>> GetListAsync()
        {
            return await RequestClient.GetAPIAsync<List<UnitTypeDto>>("unit-type");

        }

        public async Task<UnitTypeDto> CreateAsync(CreateUpdateUnitTypeDto input)
        {
            return await RequestClient.PostAPIAsync<UnitTypeDto>("unit-type",input);
        }

        public async  Task<UnitTypeDto> UpdateAsync(CreateUpdateUnitTypeDto input, Guid id)
        {
            return await RequestClient.PutAPIAsync<UnitTypeDto>($"unit-type/{id}",input);
        }

        public async Task DeleteAsync(Guid id)
        {
            await RequestClient.DeleteAPIAsync<Task>($"unit-type/{id}");
        }
    }
}