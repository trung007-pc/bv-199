using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Dashboards;
using Contract.Units;
using Domain.UnitReviewDetails;
using Domain.Units;

namespace SqlServ4r.Repository.Units
{
    public interface IUnitRepository
    {
       List<(Unit Unit, IEnumerable<UnitReviewDetail> UnitReviewDetails)> GetUnitsWithNavProperties(DashboardFilter input);
       
      Task<List<UnitWithNavProperties>> GetUnitsWithNavProperties(UnitFilter input);

    }
}