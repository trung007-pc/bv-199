using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.MyDashboards;
using Domain.DocumentFiles;
using Domain.SendingFiles;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
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

        public async Task<double> GetReadingRateOfUser(Guid userId)
        {
           var receivedfiles = await _context.SendingFiles
               .Where(x => x.ReceiverId == userId).ToListAsync();
           var rate = 100.0;
           if (receivedfiles.Count > 0)
           {
               rate = ((double)receivedfiles.Count(x => x.Status) / receivedfiles.Count )*100;
           }
           return rate;
        }
        

    }
}