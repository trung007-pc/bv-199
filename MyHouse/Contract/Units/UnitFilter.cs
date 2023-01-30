using System;
using Contract.Base;

namespace Contract.Units
{
    public class UnitFilter : FilterBase
    {
        public bool? IsActive { get; set; }
        public Guid? UnitTypeId { get; set; }
    }
}