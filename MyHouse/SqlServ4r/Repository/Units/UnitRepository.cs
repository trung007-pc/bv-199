using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.Dashboards;
using Contract.Units;
using Domain.Identity.UnitTypes;
using Domain.UnitReviewDetails;
using Domain.Units;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
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


        public List<(Unit Unit, IEnumerable<UnitReviewDetail> UnitReviewDetails)> GetUnitsWithNavProperties(DashboardFilter input)
        {
            
            var firstQueryable = from review in _context.UnitReviews.WhereIf(input.StartDay != null && input.EndDay != null,
                    x => !x.IsDeleted && x.CreationDate >= input.StartDay && x.CreationDate <= input.EndDay)
                join detail in _context.UnitReviewDetails on review.Id equals detail.UnitReviewId
                select detail;

            var secondQueryable = from unit in _context.Units.Where(x => !x.IsDeleted).WhereIf(
                        input.StartDay != null && input.EndDay != null,
                        x => x.CreationDate >= input.StartDay && x.CreationDate <= input.EndDay
                             || x.CreationDate <= input.StartDay)
                    .WhereIf(input.UnitTypeId != null, x => x.UnitTypeId == input.UnitTypeId)
                select new ValueTuple<Unit,IEnumerable<UnitReviewDetail>>(unit,firstQueryable.AsEnumerable().Where(x=>x.UnitId == unit.Id));
            
             return secondQueryable.ToList();
        }

        public async Task<List<UnitWithNavProperties>> GetUnitsWithNavProperties(UnitFilter input)
        {
            var query = from unit in _context.Units.Where(x => !x.IsDeleted)
                    .WhereIf(!input.TextFilter.IsNullOrWhiteSpace(), x => x.Name.Contains(input.TextFilter))
                    .WhereIf(input.IsActive != null, x => x.IsActive == input.IsActive)
                    .WhereIf(input.UnitTypeId !=null , x=>x.UnitTypeId == input.UnitTypeId)
                    .OrderByDescending(x=>x.Odx)
                join type in _context.UnitTypes
                    on unit.UnitTypeId equals type.Id into types
                from t in types.DefaultIfEmpty()
                select new UnitWithNavProperties
                {
                    Unit = unit,
                    UnitType = t ?? new UnitType()
                };

            return await query.ToListAsync();
        }

     
        
    }
}