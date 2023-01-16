using System;
using System.ComponentModel.DataAnnotations;

namespace Contract.Units
{
    public class CreateUpdateUnitDto
    {
        [Required(ErrorMessage = "Tên được yêu cầu")]
        public string Name { get; set; }
        public string? Note { get; set; }
        public int Odx { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreationDate { get; set; } = DateTime.Now;

        
        //File will saved in here
        public string? FileName { get; set; }
        public string? Path { get; set;}

        public string? ImageUrl { get; set; } 
          

    }
}