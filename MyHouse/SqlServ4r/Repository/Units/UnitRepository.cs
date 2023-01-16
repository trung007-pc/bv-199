using System;
using System.Collections.Generic;
using System.Linq;
using Contract.Units;
using Domain.Units;
using JetBrains.Annotations;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.Units
{
    public class UnitRepository : GenericRepository<Unit, Guid>, ITransientDependency, IUnitRepository
    {
        public UnitRepository([NotNull] DreamContext context) : base(context)
        {
        }


        public List<UnitWithNavProperties> GetUnitStatisticsByReviewDateRange(DateTime? start, DateTime? end)
        {

            var firstQueryable = from review in _context.UnitReviews.WhereIf(start != null && end != null,
                    x => !x.IsDeleted && x.CreationDate >= start && x.CreationDate <= end)
                join detail in _context.UnitReviewDetails on review.Id equals detail.UnitReviewId
                select detail;
            
            var secondQueryable = from unit in _context.Units.Where(x=>!x.IsDeleted).
                    WhereIf(start != null && end != null,
                    x => x.CreationDate >= start && x.CreationDate <= end
                    || x.CreationDate <= start)
                select new UnitWithNavProperties
                {
                   Unit = unit,
                   UnitReviewDetails = firstQueryable.AsEnumerable().Where(x=>x.UnitId == unit.Id)
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