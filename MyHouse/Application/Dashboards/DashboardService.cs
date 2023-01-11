using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.Dashboards;
using Core.Enum;
using Core.Extension;
using Domain.PartReviewDetails;
using Domain.PartReviews;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.Repository.PartReviewDetails;
using SqlServ4r.Repository.PartReviews;
using SqlServ4r.Repository.Parts;
using Volo.Abp.DependencyInjection;

namespace Application.Dashboards
{
    public class DashboardService : IDashboardService,ITransientDependency
    {
        private PartRepository _partRepository;
        private PartReviewRepository _partReviewRepository;

        public DashboardService(PartRepository partRepository,PartReviewRepository partReviewRepository)
        {
            _partRepository = partRepository;
            _partReviewRepository = partReviewRepository;
        }

        public async Task<PartReviewStatisticsDto> GetPartReviewStatisticsByDateRange(DateTime? start ,DateTime? end)
        {
            var dataItems = new List<DataItem>();
            var partsWithNavProperties = _partRepository.GetPartStatisticsByReviewDateRange(start,end);
            
            
                foreach (var items in partsWithNavProperties)
                {   
                    var details = items.PartReviewDetails
                        .Where(x =>x.Rate > 0).ToList();
            
                    dataItems.Add(new DataItem()
                    {
                        Label = items.Part.Name,
                        Value = (details.Count() != 0
                            ? details.Sum(x => x.Rate) / details.Count()
                            : 0)
                    });
                }
            
               
                
            var orderedDataItems = dataItems.OrderBy(x => x.Value).ToList();
            var reviewCount = partsWithNavProperties.SelectMany(x => x.PartReviewDetails).GroupBy(x=>x.PartReviewId).Count();
            var allDetails = partsWithNavProperties.SelectMany(x => x.PartReviewDetails);
            
            return new PartReviewStatisticsDto()
                {PartReviewItems = orderedDataItems, TotalReview = reviewCount, DetailPartReviewItems =  GetRatingStats(allDetails)};
            

        }
        
        
  
        
        
        
        

        private static List<DataItem> GetRatingStats(IEnumerable<PartReviewDetail> allDetails)
        {
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
