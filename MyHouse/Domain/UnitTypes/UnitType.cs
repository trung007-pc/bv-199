using System;
using System.Collections.Generic;
using Domain.Units;

namespace Domain.UnitTypes
{
    public class UnitType
    {
        public Guid Id { get; set; }
        
        public string Name { get; set;}
        
        public ICollection<Unit> Units { get; set; }

    }
}