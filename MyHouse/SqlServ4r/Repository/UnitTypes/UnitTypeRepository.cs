using System;
using Domain.Units;
using Domain.UnitTypes;
using JetBrains.Annotations;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.UnitTypes
{
    public class UnitTypeRepository : GenericRepository<UnitType, Guid>, ITransientDependency
    {
        public UnitTypeRepository([NotNull] DreamContext context) : base(context)
        {
        }
    }
}