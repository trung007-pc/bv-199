using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.Dashboards;
using Core.Enum;
using Core.Extension;
using Domain.UnitReviewDetails;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.Repository.UnitReviews;
using SqlServ4r.Repository.Units;
using Volo.Abp.DependencyInjection;

namespace Application.Dashboards
{
    public class DashboardService : IDashboardService,ITransientDependency
    {
        private UnitRepository _unitRepository;
        private UnitReviewRepository _unitReviewRepository;

        public DashboardService(UnitRepository unitRepository,UnitReviewRepository unitReviewRepository)
        {
            _unitRepository = unitRepository;
            _unitReviewRepository = unitReviewRepository;
        }

        public async Task<UnitReviewStatisticsDto> GetUnitReviewStatisticsByDateRange(DateTime? start ,DateTime? end)
        {
            
            var dataItems = new List<DataItem>();
            var unitsWithNavProperties = _unitRepository.GetUnitStatisticsByReviewDateRange(start,end);
            
            
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
                {UnitReviewItems = orderedDataItems, TotalReview = reviewCount, DetailUnitReviewItems =  GetRatingStats(allDetails)};
            

        }
        
        
  
        
        
        
        

        private static List<DataItem> GetRatingStats(IEnumerable<UnitReviewDetail> allDetails)
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
