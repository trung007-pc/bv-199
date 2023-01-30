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
        private readonly UnitRepository _unitRepository;
        public UnitTypeService(UnitTypeRepository unitTypeRepository,UnitRepository unitRepository)
        {
            _unitTypeRepository = unitTypeRepository;
            _unitRepository = unitRepository;
        }
        public async Task<List<UnitTypeDto>> GetListAsync()
        {
            var types = await _unitTypeRepository.ToListAsync();
            var typesDto = ObjectMapper.Map<List<UnitType>, List<UnitTypeDto>>(types);
            return typesDto;
        }
        
        

        public async Task<UnitTypeDto> CreateAsync(CreateUpdateUnitTypeDto input)
        {
            await _checkDuplicateNameAtCreating(input.Name);
            var type = ObjectMapper.Map<CreateUpdateUnitTypeDto, UnitType>(input);
            await _unitTypeRepository.AddAsync(type);

             return ObjectMapper.Map<UnitType, UnitTypeDto>(type);
        }

        public async Task<UnitTypeDto> UpdateAsync(CreateUpdateUnitTypeDto input, Guid id)
        {
            DbUpdateConcurrencyException a = new DbUpdateConcurrencyException();
           
            var item = await _unitTypeRepository.FirstOrDefaultAsync(x => x.Id == id);
            
            if(item is null)  throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            
            await _checkDuplicateNameAtUpdating(input.Name,id);
            var type = ObjectMapper.Map(input,item);
            _unitTypeRepository.Update(type);
            
            return ObjectMapper.Map<UnitType, UnitTypeDto>(type);
        }
        
        private async Task _checkDuplicateNameAtCreating(string name)
        {
            var trimedName = name.Trim();
            var exist = await _unitTypeRepository.GetQueryable().AnyAsync(x => x.Name == trimedName);

            if (exist) throw new GlobalException(HttpMessage.DuplicateName, HttpStatusCode.BadRequest);
        }
        
        private async Task _checkDuplicateNameAtUpdating(string name,Guid id)
        {
            var trimedName = name.Trim();
            var exist = await _unitTypeRepository.GetQueryable().
                AnyAsync(x => x.Name == trimedName && x.Id != id);

            if (exist) throw new GlobalException(HttpMessage.DuplicateName, HttpStatusCode.BadRequest);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _unitTypeRepository.FirstOrDefaultAsync(x => x.Id == id);
            if(item is null)  throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);

           var units = await _unitRepository.GetListAsync(x => x.UnitTypeId == id);
           
           var updatedUnits = units.Select(x =>
           {
               x.UnitTypeId = null;
               return x;
           }).ToList();
           
           _unitRepository.UpdateRange(updatedUnits);
           
            _unitTypeRepository.Remove(item);
        }
    }
}