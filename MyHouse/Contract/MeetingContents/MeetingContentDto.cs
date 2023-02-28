﻿using System;

namespace Contract.MeetingContents
{
    public class MeetingContentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Node { get; set; }
        public DateTime CreationTime { get; set; } =DateTime.Now;
        public bool IsPublic { get; set; }
        public Guid CreatedBy { get; set; }

        
        
        public string? FileName { get; set; }
        public string? Path { get; set;}
        public string? Url { get; set;}
    }
}