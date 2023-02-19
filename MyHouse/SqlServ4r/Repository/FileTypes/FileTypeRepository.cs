using System;
using Domain.FileTypes;
using JetBrains.Annotations;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.FileTypes
{
    public class FileTypeRepository : GenericRepository<FileType, Guid>, ITransientDependency
    {
        public FileTypeRepository([NotNull] DreamContext context) : base(context)
        {
        }
    }
}