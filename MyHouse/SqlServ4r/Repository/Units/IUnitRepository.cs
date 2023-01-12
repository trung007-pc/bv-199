using System;
using System.Collections.Generic;
using Contract.Units;

namespace SqlServ4r.Repository.Units
{
    public interface IUnitRepository
    {
       List<UnitWithNavProperties> GetUnitStatisticsByReviewDateRange(DateTime? start , DateTime? end);
    }
}