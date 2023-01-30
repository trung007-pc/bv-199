using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.UnitTypes;

namespace Contract.Units
{
    public interface IUnitService
    {
        Task<UnitDto> CreateAsync(CreateUpdateUnitDto input);
        Task<UnitDto> UpdateAsync(CreateUpdateUnitDto input,Guid id);
        Task DeleteAsync(Guid id);
        Task<List<UnitWithNavPropertiesDto>> GetListWithNavPropertiesAsync(UnitFilter input);
        
        Task<List<UnitTypeDto>> LookUpUnitTypes();

    }
}