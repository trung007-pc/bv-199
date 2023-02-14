using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Contract.UnitReviewDetails;
using Contract.UnitReviews;
using Contract.Units;
using Core.Const;
using Core.Exceptions;
using Domain.UnitReviewDetails;
using Domain.UnitReviews;
using Domain.Units;
using SqlServ4r.Repository.UnitReviewDetails;
using SqlServ4r.Repository.UnitReviews;
using SqlServ4r.Repository.Units;
using Volo.Abp.DependencyInjection;

namespace Application.UnitReviews
{
    public class UnitReviewService : ServiceBase, IUnitReviewService, ITransientDependency
    {
        private readonly UnitReviewRepository _unitReviewRepository;

        private readonly UnitReviewDetailRepository _unitReviewDetailRepository;
        


        public UnitReviewService(UnitReviewRepository unitReviewRepository,
            UnitReviewDetailRepository unitReviewDetailRepository)
        {
            _unitReviewRepository = unitReviewRepository;
            _unitReviewDetailRepository = unitReviewDetailRepository;
           
        }

        public async Task<List<UnitReviewDto>> GetListWithCalculatingAverageAsync(UnitReviewFilter input)
        {
            var reviewsWithNav = await _unitReviewRepository.GetListWithNavigationPropertiesAsync(input);
            var reviewsWithNavDto = new List<UnitReviewDto>();
            
            foreach (var item in reviewsWithNav)
            {
                var dto = ObjectMapper.Map<UnitReview, UnitReviewDto>(item);
                item.UnitReviewDetails = item.UnitReviewDetails.Where(x => x.Rate > 0).ToList();
                if (item.UnitReviewDetails.Count > 0)
                {
                    dto.AveragePoint = item.UnitReviewDetails.Sum(x => x.Rate) / item.UnitReviewDetails.Count;
                }

                reviewsWithNavDto.Add(dto);
            }
            return reviewsWithNavDto;
        }


        public async Task<UnitReviewDto> CreateAsync(CreateUpdateUnitReviewDto input)
        {
            var review = ObjectMapper.Map<CreateUpdateUnitReviewDto, UnitReview>(input);
            await _unitReviewRepository.AddAsync(review);
            return ObjectMapper.Map<UnitReview, UnitReviewDto>(review);
        }

        public async Task<UnitReviewDto> UpdateAsync(CreateUpdateUnitReviewDto input, Guid Id)
        {
            var review = await _unitReviewRepository.FirstOrDefaultAsync(x => x.Id == Id);

            if (review == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }

            ObjectMapper.Map(input, review);
            _unitReviewRepository.Update(review);

            return ObjectMapper.Map<UnitReview, UnitReviewDto>(review);
        }

        public async Task<UnitReviewDto> CreateReviewWithDetailsAsync(List<CreateUpdateUnitReviewDetailDto> inputs)
        {
            var review = new UnitReview();
            await _unitReviewRepository.AddAsync(review);
            var details = ObjectMapper.Map<List<CreateUpdateUnitReviewDetailDto>, List<UnitReviewDetail>>(inputs);

            foreach (var item in details)
            {
                item.UnitReviewId = review.Id;
            }

            await _unitReviewDetailRepository.AddRangeAsync(details);

            return ObjectMapper.Map<UnitReview, UnitReviewDto>(review); 
        }

        public async Task DeleteAsync(Guid reviewId)
        {
            var review = await _unitReviewRepository.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (review == null)
            {
                throw new GlobalException(HttpMessage.NotFound, HttpStatusCode.BadRequest);
            }

            review.IsDeleted = true;
            _unitReviewRepository.Update(review);
        }
        
    }
}