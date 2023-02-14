using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contract.UnitTypes;
using Core.Const;
using Core.Exceptions;
using Domain.Identity.UnitTypes;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.Repository.Units;
using SqlServ4r.Repository.UnitTypes;
using Volo.Abp.DependencyInjection;

namespace Application.UnitTypes
{
    public class UnitTypeService : ServiceBase, IUnitTypeService,ITransientDependency
    {
        private readonly UnitTypeRepository _unitTypeRepository;
        public UnitTypeService(UnitTypeRepository unitTypeRepository)
        {
            _unitTypeRepository = unitTypeRepository;
        }
        
        public async Task<List<UnitTypeDto>> GetListAsync()
        {
            var types = await _unitTypeRepository.ToListAsync();
            var typesDto = ObjectMapper.Map<List<UnitType>, List<UnitTypeDto>>(types);
            return typesDto;
        }
        
        public async Task<UnitTypeDto> CreateAsync(CreateUpdateUnitTypeDto input)
        {
            input.Name = input.Name.Trim();
            var type = ObjectMapper.Map<CreateUpdateUnitTypeDto, UnitType>(input);
            await _unitTypeRepository.AddAsync(type);
            return ObjectMapper.Map<UnitType, UnitTypeDto>(type);
        }

        public async Task<UnitTypeDto> UpdateAsync(CreateUpdateUnitTypeDto input, Guid id)
        {
            input.Name = input.Name.Trim();
            var item = await _unitTypeRepository.FirstOrDefaultAsync(x => x.Id == id);
            
            if(item is null)  throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            
            var type = ObjectMapper.Map(input,item);
            _unitTypeRepository.Update(type);
            
            return ObjectMapper.Map<UnitType, UnitTypeDto>(type);
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var item = await _unitTypeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if(item is null)  throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            _unitTypeRepository.Remove(item);
        }
    }
}