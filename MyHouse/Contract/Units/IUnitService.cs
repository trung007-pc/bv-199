using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.Units
{
    public interface IUnitService
    {
        Task<UnitDto> CreateAsync(CreateUpdateUnitDto input);
        Task<UnitDto> UpdateAsync(CreateUpdateUnitDto input,Guid id);
        Task DeleteAsync(Guid id);
        Task<List<UnitDto>> GetListAsync();
        
        Task<List<UnitDto>> GetListAsync(UnitFilter input);

    }
}