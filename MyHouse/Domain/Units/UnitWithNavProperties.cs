using Domain.Identity.UnitTypes;

namespace Domain.Units
{
    public class UnitWithNavProperties
    {
        public Unit Unit { get; set; }
        
        public UnitType UnitType { get; set;}
    }
}