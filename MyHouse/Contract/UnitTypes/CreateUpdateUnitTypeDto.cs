using System;
using System.ComponentModel.DataAnnotations;

namespace Contract.UnitTypes
{
    public class CreateUpdateUnitTypeDto
    {
        [Required(ErrorMessage = "Tên được yêu cầu")]
        [MinLength(5,ErrorMessage = "At least 5 character")]
        public string Name { get; set;}
    }
}