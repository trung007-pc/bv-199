using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contract.Units;
using Core.Const;
using Core.Exceptions;
using Domain.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlServ4r.Repository.Units;
using Volo.Abp.DependencyInjection;

namespace Application.Units
{
    public class UnitService : ServiceBase, IUnitService, ITransientDependency
    {
        public UnitRepository _unitRepository;
        private IConfiguration _configuration;


        public UnitService(UnitRepository unitRepository,IConfiguration configuration)
        {
            _unitRepository = unitRepository;
            _configuration = configuration;
        }


        public async Task<UnitDto> CreateAsync(CreateUpdateUnitDto input)
        {
            var unit = ObjectMapper.Map<CreateUpdateUnitDto, Unit>(input);
            if (unit.ImageUrl == null)
            {
                unit.ImageUrl = _configuration["Media:Default_Image_Path"];
            }

            var exist = _unitRepository.GetQueryable().Any(x => x.Name == input.Name && !x.IsDeleted);
            
            if(exist) throw new GlobalException(HttpMessage.DuplicateName, HttpStatusCode.BadRequest);
            
            await _unitRepository.AddAsync(unit);
            return ObjectMapper.Map<Unit,UnitDto>(unit);
        }
        

        public async Task<UnitDto> UpdateAsync(CreateUpdateUnitDto input, Guid id)
        {
            var unit = await _unitRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (unit == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }
            unit = ObjectMapper.Map(input, unit);
            _unitRepository.Update(unit);
            
            return ObjectMapper.Map<Unit,UnitDto>(unit);
        }

        public async Task DeleteAsync(Guid id)
        {
            var unit = await _unitRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (unit == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }
            
            unit.IsDeleted = true;
            _unitRepository.Update(unit);
        }

        public async Task<List<UnitDto>> GetListAsync()
        {
            var units = await _unitRepository.GetListAsync(x=>x.IsDeleted == false);
            var unitsDto = ObjectMapper.Map<List<Unit>,List<UnitDto>>(units);
            AttachIndex(unitsDto);
            return unitsDto;
        }

        public async Task<List<UnitDto>> GetListAsync(UnitFilter input)
        {
          var units =  await _unitRepository.GetQueryable().Where(x => x.IsDeleted == false)
                .WhereIf(!input.TextFilter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.TextFilter))
                .WhereIf(input.IsActive != null, x => x.IsActive == input.IsActive).ToListAsync();

          var unitsDto = ObjectMapper.Map<List<Unit>,List<UnitDto>>(units);
            AttachIndex(unitsDto);
            return unitsDto;
        }
    }
}