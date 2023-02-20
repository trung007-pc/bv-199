using System;
using Domain.SendingFiles;
using JetBrains.Annotations;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.SendingFiles
{
    public class SendingFileRepository : GenericRepository<SendingFile, Guid>, ITransientDependency
    {
        public SendingFileRepository([NotNull] DreamContext context) : base(context)
        {
        }
    }
}