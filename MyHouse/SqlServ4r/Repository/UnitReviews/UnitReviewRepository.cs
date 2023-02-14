using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.UnitReviews;
using Domain.UnitReviews;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.UnitReviews
{
    public class UnitReviewRepository : GenericRepository<UnitReview,Guid>,ITransientDependency,IUnitReviewRepository
    {
        public UnitReviewRepository([NotNull] DreamContext context) : base(context)
        {
        }

        
        public async Task<List<UnitReview>> GetListWithNavigationPropertiesAsync(UnitReviewFilter input)
        {
            var items = await _context.UnitReviews.Where(x=>!x.IsDeleted)
                .WhereIf(input.StartDay!= null && input.EndDay!=null ,
                    x=>x.CreationDate >=input.StartDay && x.CreationDate <= input.EndDay)
                .
                Include(x => x.UnitReviewDetails).ToListAsync();

            return items;
        }
    }
}