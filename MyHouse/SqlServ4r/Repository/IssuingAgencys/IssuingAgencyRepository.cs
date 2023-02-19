using System;
using Domain.IssuingAgencys;
using JetBrains.Annotations;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.IssuingAgencys
{ 
    public class IssuingAgencyRepository : GenericRepository<IssuingAgency, Guid>, ITransientDependency
    {
        public IssuingAgencyRepository([NotNull] DreamContext context) : base(context)
        {
        }
    }
}