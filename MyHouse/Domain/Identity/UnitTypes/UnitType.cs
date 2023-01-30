using System;
using System.Collections.Generic;
using Domain.Units;
using Microsoft.EntityFrameworkCore;

namespace Domain.Identity.UnitTypes
{
    public class UnitType
    {
        public Guid Id { get; set; }
        
        public string Name { get; set;}
        
        public ICollection<Unit> Units { get; set; }

    }
}