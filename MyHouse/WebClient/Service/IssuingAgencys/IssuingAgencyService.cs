using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.IssuingAgencys;
using WebClient.RequestHttp;

namespace WebClient.Service.IssuingAgencys
{
    public class IssuingAgencyService
    {
        public IssuingAgencyService()
        {
            
        }
        
        public async Task<List<IssuingAgencyDto>> GetListAsync()
        {
            return await RequestClient.GetAPIAsync<List<IssuingAgencyDto>>("issuing-agency");
        }

        public async Task<IssuingAgencyDto> CreateAsync(CreateUpdateIssuingAgencyDto input)
        {
            return await RequestClient.PostAPIAsync<IssuingAgencyDto>("issuing-agency",input);

        }

        public async Task<IssuingAgencyDto> UpdateAsync(CreateUpdateIssuingAgencyDto input, Guid id)
        {
            return await RequestClient.PutAPIAsync<IssuingAgencyDto>($"issuing-agency/{id}" , input);

        }

        public async Task DeleteAsync(Guid id)
        { 
            await RequestClient.DeleteAPIAsync<Task>($"issuing-agency/{id}");
        }
    }
}