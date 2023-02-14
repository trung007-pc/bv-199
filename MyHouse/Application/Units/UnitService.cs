﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contract.Units;
using Contract.UnitTypes;
using Core.Const;
using Core.Exceptions;
using Domain.Identity.UnitTypes;
using Domain.Units;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SqlServ4r.Repository.Units;
using SqlServ4r.Repository.UnitTypes;
using Volo.Abp.DependencyInjection;

namespace Application.Units
{
    public class UnitService : ServiceBase, IUnitService, ITransientDependency
    {
        private readonly UnitRepository _unitRepository;
        private readonly UnitTypeRepository _unitTypeRepository;
        private readonly  IConfiguration _configuration;


        public UnitService(UnitRepository unitRepository,UnitTypeRepository unitTypeRepository,IConfiguration configuration)
        {
            _unitRepository = unitRepository;
            _configuration = configuration;
            _unitTypeRepository = unitTypeRepository;
        }


        public async Task<UnitDto> CreateAsync(CreateUpdateUnitDto input)
        {
            input.Name = input.Name.Trim();
            var unit = ObjectMapper.Map<CreateUpdateUnitDto, Unit>(input);
            if (unit.ImageUrl == null)
            {
                unit.ImageUrl = _configuration["Media:Default_Image_Path"];
            }
            
            await _unitRepository.AddAsync(unit);
            return ObjectMapper.Map<Unit,UnitDto>(unit);
        }

        
        public async Task<UnitDto> UpdateAsync(CreateUpdateUnitDto input, Guid id)
        {

            input.Name = input.Name.Trim();
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

        public async Task<List<UnitWithNavPropertiesDto>> GetListWithNavPropertiesAsync(UnitFilter input)
        {
            var units = await _unitRepository.GetUnitsWithNavProperties(input);
            var unitsDto = ObjectMapper.Map<List<UnitWithNavProperties>,List<UnitWithNavPropertiesDto>>(units);
            return unitsDto;
        }
        
        public async Task<List<UnitTypeDto>> LookUpUnitTypes()
        {
           var types = await _unitTypeRepository.ToListAsync();
            return  ObjectMapper.Map<List<UnitType>,List<UnitTypeDto>>(types);
        }
    }
}