﻿using System.ComponentModel.DataAnnotations;

namespace Contract.Units
{
    public class CreateUpdateUnitDto
    {
        [Required(ErrorMessage = "Tên được yêu cầu")]
        public string Name { get; set; }
        public string? Note { get; set; }
        public int Odx { get; set; }
        public bool IsActive { get; set; } = true;
        
        //File will saved in here
        public string? FileName { get; set; }
        public string? Path { get; set;}

        public string? ImageUrl { get; set; } =
            "https://localhost:7093/StaticFiles/Images/Default/Logo_BenhVien199.png";

    }
}