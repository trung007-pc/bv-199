using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.UnitTypes
{
    public interface IUnitTypeService
    {
        Task<List<UnitTypeDto>> GetListAsync();
        Task<UnitTypeDto> CreateAsync(CreateUpdateUnitTypeDto input);
        Task<UnitTypeDto> UpdateAsync(CreateUpdateUnitTypeDto input,Guid id);
        Task DeleteAsync(Guid id);
    }
}