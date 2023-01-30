using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.Dashboards;
using Contract.UnitReviewDetails;
using Contract.Units;
using Contract.UnitTypes;
using Core.Enum;
using Core.Extension;
using Domain.Identity.UnitTypes;
using Domain.UnitReviewDetails;
using Domain.Units;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.Repository.UnitReviews;
using SqlServ4r.Repository.Units;
using SqlServ4r.Repository.UnitTypes;
using Volo.Abp.DependencyInjection;

namespace Application.Dashboards
{
    public class DashboardService :ServiceBase, IDashboardService,ITransientDependency
    {
        private readonly UnitRepository _unitRepository;
        private readonly UnitTypeRepository _unitTypeRepository;

        public DashboardService(UnitRepository unitRepository
        ,UnitTypeRepository unitTypeRepository)
        {
            _unitRepository = unitRepository;
            _unitTypeRepository = unitTypeRepository;
        }

        public async Task<UnitReviewStatisticsDto> GetUnitReviewStatistics(DashboardFilter input)
        {
            
            var dataItems = new List<DataItem>();
            var unitsWithNavProperties = _unitRepository.GetUnitsWithNavProperties(input);
            
                foreach (var items in unitsWithNavProperties)
                {   
                    var details = items.UnitReviewDetails
                        .Where(x =>x.Rate > 0).ToList();
            
                    dataItems.Add(new DataItem()
                    {
                        Label = items.Unit.Name,
                        Value = (details.Count() != 0
                            ? details.Sum(x => x.Rate) / details.Count()
                            : 0)
                    });
                }
            
               
                
            var orderedDataItems = dataItems.OrderBy(x => x.Value).ToList();
            var reviewCount = unitsWithNavProperties.SelectMany(x => x.UnitReviewDetails).GroupBy(x=>x.UnitReviewId).Count();
            var allDetails = unitsWithNavProperties.SelectMany(x => x.UnitReviewDetails);
            
            return new UnitReviewStatisticsDto()
                {UnitReviewItems = orderedDataItems, TotalReview = reviewCount, DetailUnitReviewItems =  _getRatingStats(allDetails)};
            

        }

        public async Task<List<UnitTypeDto>> LookUpUnitTypes()
        {
           var types = await _unitTypeRepository.ToListAsync();
           return ObjectMapper.Map<List<UnitType>,List<UnitTypeDto>>(types);
        }


        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        private static List<DataItem> _getRatingStats(IEnumerable<UnitReviewDetail> allDetails)
        {
            if (allDetails.Count() == 0) return new List<DataItem>();
            var detailItems = new List<DataItem>()
            {
                new DataItem()
                {
                    Label = RatingType.Unknown.GetDescriptionOrName(),
                    Value = allDetails.Count(x => x.Rate == 0)
                },
                new DataItem()
                {
                    Label = RatingType.Bad.GetDescriptionOrName(),
                    Value = allDetails.Count(x => x.Rate == 1)
                },
                new DataItem()
                {
                    Label = RatingType.Normal.GetDescriptionOrName(),
                    Value = allDetails.Count(x => x.Rate == 2)
                },
                new DataItem()
                {
                    Label = RatingType.NotBad.GetDescriptionOrName(),
                    Value = allDetails.Count(x => x.Rate == 3)
                },
                new DataItem()
                {
                    Label = RatingType.Good.GetDescriptionOrName(),
                    Value = allDetails.Count(x => x.Rate == 4)
                },
                new DataItem()
                {
                    Label = RatingType.VeryGood.GetDescriptionOrName(),
                    Value = allDetails.Count(x => x.Rate == 5)
                },
            };

            return detailItems;
        }
    }
    }
