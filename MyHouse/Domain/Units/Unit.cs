using System;
using System.Collections.Generic;
using Domain.UnitReviewDetails;

namespace Domain.Units
{
    public class Unit
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string? Note { get; set; }
        public int Odx { get; set; }
        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
        
        public DateTime CreationDate { get; set; } = DateTime.Now;

        
        
        //media
        public string? FileName { get; set; }
        public string? Path { get; set;}
        public string? ImageUrl { get; set;}

        public IList<UnitReviewDetail> UnitReviewDetails { get; set; }
    }
}