using System;

namespace Contract.Units
{
    public class UnitDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Index { get; set;}
        public string Name { get; set; }
        public string? Note { get; set; }
        public int Odx { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; } 
        
        public string? FileName { get; set; }
        public string? ImageUrl { get; set; } 
        
    }
}