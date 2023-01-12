using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.UnitReviewDetails;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.UnitReviewDetails
{
    public class UnitReviewDetailRepository : GenericRepository<UnitReviewDetail,Guid>,ITransientDependency,IUnitReviewDetailRepository
    {
        public UnitReviewDetailRepository([NotNull] DreamContext context) : base(context)
        {
        }

        public async Task<List<UnitReviewDetail>> GetListWithNavigationPropertiesAsync(Guid reviewId)
        {
          var details = await  _context.UnitReviewDetails.Where(x=>x.UnitReviewId == reviewId).
              Include(x => x.Unit).ToListAsync();
          return details;
        }
    }
}