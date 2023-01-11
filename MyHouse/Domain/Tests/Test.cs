using System;

namespace Domain.Tests
{
    public class Test
    {
        public Guid Id { get; set; } 

        public string Name { get; set; }
        public Media  Media { get; set; }
    }

    public class Media
    {
        public string File { get; set; }
        public int ox { get; set; }
    }
    
    
}