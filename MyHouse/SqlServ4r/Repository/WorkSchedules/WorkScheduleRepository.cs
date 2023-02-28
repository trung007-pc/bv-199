using System;
using Domain.WorkSchedules;
using JetBrains.Annotations;
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
    }
}