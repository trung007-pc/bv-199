using System;
using Domain.FileFolders;
using JetBrains.Annotations;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.FileFolders
{
    public class FileFolderRepository : GenericRepository<FileFolder, Guid>, ITransientDependency
    {
        public FileFolderRepository([NotNull] DreamContext context) : base(context)
        {
        }
    }
}