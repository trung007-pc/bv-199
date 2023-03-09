using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Domain.WorkSchedules;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.WorkSchedules
{
    public class WorkScheduleRepository : GenericRepository<WorkSchedule, Guid>, ITransientDependency
    {
        public WorkScheduleRepository([NotNull] DreamContext context) : base(context)
        {
        }
        
       public  async Task<List<WorkSchedule>> GetListAsync(
            DateTime? from = null,
            DateTime? to = null
        )
        {
            var query = _context.WorkSchedules.WhereIf(from.HasValue && to.HasValue,
                x=>   x.CreationTime >= from && x.CreationTime <= to)
                .OrderByDescending(x=>x.StartDay);
            
            return await query.ToListAsync();
        }
    }
}