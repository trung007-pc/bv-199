using System;
using System.ComponentModel.DataAnnotations;

namespace Contract.MeetingContents
{
    public class CreateUpdateMeetingContentDto
    {
        [Required(ErrorMessage = "Name is required")]

        public string Name { get; set; }
        
        [Required(ErrorMessage = "Node is required")]
        public string Node { get; set; }
        public DateTime CreationTime { get; set; } =DateTime.Now;
        public bool IsPublic { get; set; } = true;
        public Guid CreatedBy { get; set; }

        
        
        public string? FileName { get; set; }
        public string? Path { get; set;}
        public string? Url { get; set;}
        public string? Extentions { get; set; }

    }
}