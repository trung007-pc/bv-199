using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Dashboards;
using Contract.Parts;

namespace SqlServ4r.Repository.Parts
{
    public interface IPartRepository
    {
       List<PartWithNavProperties> GetPartStatisticsByReviewDateRange(DateTime? start , DateTime? end);
    }
}