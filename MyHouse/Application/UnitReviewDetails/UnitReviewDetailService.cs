using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Contract.UnitReviewDetails;
using Core.Const;
using Core.Exceptions;
using Domain.UnitReviewDetails;
using SqlServ4r.Repository.UnitReviewDetails;
using Volo.Abp.DependencyInjection;

namespace Application.UnitReviewDetails
{
    public class UnitReviewDetailService:ServiceBase,IUnitReviewDetailService,ITransientDependency
    {
        private UnitReviewDetailRepository _unitReviewDetailRepository;

        
        public UnitReviewDetailService(UnitReviewDetailRepository unitReviewDetailRepository)
        {
            _unitReviewDetailRepository = unitReviewDetailRepository;
        }
        
        public async Task<UnitReviewDetailDto> CreateAsync(CreateUpdateUnitReviewDetailDto input)
        {
            var detail = ObjectMapper.Map<CreateUpdateUnitReviewDetailDto,UnitReviewDetail>(input);
            await _unitReviewDetailRepository.AddAsync(detail);
            return ObjectMapper.Map<UnitReviewDetail,UnitReviewDetailDto>(detail);

        }
        

        public async Task<UnitReviewDetailDto> UpdateAsync(CreateUpdateUnitReviewDetailDto input,Guid id)
        {
            var detail = await _unitReviewDetailRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (detail == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }

            detail = ObjectMapper.Map(input, detail);
            _unitReviewDetailRepository.Update(detail);
            
            return ObjectMapper.Map<UnitReviewDetail,UnitReviewDetailDto>(detail);
        }


        public async Task DeleteAsync(Guid id)
        {
            var detail = await _unitReviewDetailRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (detail == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }
            _unitReviewDetailRepository.Remove(detail);
            
        }

        public async Task<List<UnitReviewDetailDto>> GetListAsync()
        {
            var details = await _unitReviewDetailRepository.ToListAsync();
            return ObjectMapper.Map<List<UnitReviewDetail>,List<UnitReviewDetailDto>>(details);
        }
        

        public async Task<List<UnitReviewDetailDto>> GetDetailsByReviewIdAsync(Guid reviewId)
        {
            var details = await _unitReviewDetailRepository.GetListWithNavigationPropertiesAsync(reviewId);
            var detailsDto = new List<UnitReviewDetailDto>();
            
            var index = 1;
            foreach (var item in details)
            {
                var dto = ObjectMapper.Map<UnitReviewDetail,UnitReviewDetailDto>(item);
                dto.UnitName = item.Unit.Name;
                dto.Index = index;
                index++;
                detailsDto.Add(dto);
            }
            return detailsDto;
        }
        
    }
}