using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.Dashboards;
using Contract.Parts;
using Domain.PartReviewDetails;
using Domain.PartReviews;
using Domain.Parts;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.Parts
{
    public class PartRepository : GenericRepository<Part, Guid>, ITransientDependency, IPartRepository
    {
        public PartRepository([NotNull] DreamContext context) : base(context)
        {
        }


        public List<PartWithNavProperties> GetPartStatisticsByReviewDateRange(DateTime? start, DateTime? end)
        {

            var firstQueryable = from review in _context.PartReviews.WhereIf(start != null && end != null,
                    x => !x.IsDeleted && x.CreationDate >= start && x.CreationDate <= end)
                join detail in _context.PartReviewDetails on review.Id equals detail.PartReviewId
                select detail;
            
            var secondQueryable = from part in _context.Parts.Where(x => !x.IsDeleted)
                select new PartWithNavProperties
                {
                    Part = part,
                    PartReviewDetails = firstQueryable.AsEnumerable().Where(x=>x.PartId == part.Id)
                };


             return secondQueryable.ToList();
             

        }
        
        
        // var queryable = from review in _context.PartReviews.WhereIf(start != null && end != null,
        //         x =>!x.IsDeleted && x.CreationDate >= start && x.CreationDate <= end)
        //     join detail in _context.PartReviewDetails  on review.Id equals detail.PartReviewId
        //     join part in _context.Parts.Where(x=>!x.IsDeleted) on detail.PartId equals part.Id
        //     select new PartWithNavProperties()
        //     {
        //         Part = part,
        //         PartReview = review,
        //         PartReviewDetail = detail
        //     };
        //
        //
        // return queryable.ToList();

       
    }
}