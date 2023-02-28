using System;
using Domain.MeetingContents;
using JetBrains.Annotations;
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
    }
}