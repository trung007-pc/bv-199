using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contract.IssuingAgencys;
using Core.Const;
using Core.Exceptions;
using Domain.IssuingAgencys;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.Repository.IssuingAgencys;
using Volo.Abp.DependencyInjection;

namespace Application.IssuingAgencys
{
    public class IssuingAgencyService : ServiceBase,ITransientDependency
    {
         private readonly  IssuingAgencyRepository  _issuingAgencyRepository;
         
        public IssuingAgencyService(IssuingAgencyRepository issuingAgencyRepository)
        {
             _issuingAgencyRepository = issuingAgencyRepository;
        }
        
        
        public async Task<IssuingAgencyDto> CreateAsync(CreateUpdateIssuingAgencyDto input)
        {   
            (input.Name, input.Code) = TrimText(input.Name, input.Code);

            var agencys = ObjectMapper.Map<CreateUpdateIssuingAgencyDto, IssuingAgency>(input);
            await  _issuingAgencyRepository.AddAsync(agencys);
            return ObjectMapper.Map<IssuingAgency,IssuingAgencyDto>(agencys);
        }



        public async Task<IssuingAgencyDto> UpdateAsync(CreateUpdateIssuingAgencyDto input, Guid id)
        {
            (input.Name, input.Code) = TrimText(input.Name, input.Code);

            var item = await  _issuingAgencyRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (item is null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }
            
            var agencys = ObjectMapper.Map(input,item);
            await  _issuingAgencyRepository.UpdateAsync(agencys);

            return ObjectMapper.Map<IssuingAgency,IssuingAgencyDto>(agencys);
        }

 

        public async Task DeleteAsync(Guid id)
        {
            var agencys = await  _issuingAgencyRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (agencys is null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }
            
            _issuingAgencyRepository.Remove(agencys);
        }

        public async Task<List<IssuingAgencyDto>> GetListAsync()
        {
            var forders = await  _issuingAgencyRepository.GetQueryable()
                .OrderBy(x=>x.ODX).ToListAsync();
               
            return ObjectMapper.Map<List<IssuingAgency>, List<IssuingAgencyDto>>(forders);
        }
        
        private (string Name, string? Code) TrimText(string name, string code)
        {
            return (name.Trim(), code?.Trim().ToUpper());
        }
    }
}