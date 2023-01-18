using System;
using System.Collections.Generic;
using Domain.Units;

namespace Domain.Identity.UnitTypes
{
    public class UnitType
    {
        public Guid Id { get; set; }
        
        public string Name { get; set;}
        
        public IList<Unit> Units { get; set; }

    }
}