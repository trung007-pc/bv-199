using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.MeetingContents;
using Contract.WorkSchedules;
using Domain.MeetingContents;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.MeetingContents
{
    public class MeetingContentRepository : GenericRepository<MeetingContent, Guid>, ITransientDependency
    {
        public MeetingContentRepository([NotNull] DreamContext context) : base(context)
        {
        }


       public async Task<List<MeetingContent>> GetListAsync(
            DateTime? from = null,
            DateTime? to = null,
            bool? isPublic = null
        )
        {
            var query = _context.MeetingContents.WhereIf(from.HasValue && to.HasValue, x => x.IsPublic
                    && x.CreationTime >= from && x.CreationTime <= to)
                .WhereIf(isPublic is not null, x => x.IsPublic == isPublic)
                .OrderByDescending(x=>x.CreationTime);
            return await query.ToListAsync();
        }
    }
}