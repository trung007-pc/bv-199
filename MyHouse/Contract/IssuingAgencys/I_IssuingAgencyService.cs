using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contract.IssuingAgencys
{
    public interface I_IssuingAgencyService
    {
        Task< IssuingAgencyDto> CreateAsync(CreateUpdateIssuingAgencyDto input);
        Task< IssuingAgencyDto> UpdateAsync(CreateUpdateIssuingAgencyDto input,Guid id);
        Task DeleteAsync(Guid id);
        Task<List<IssuingAgencyDto>> GetListAsync();
    }
}