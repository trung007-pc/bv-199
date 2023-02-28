using System;

namespace Contract.MyDashboards
{
    public class GlobalStatisticFilter
    {
        public Guid UserId { get; set; }
        public int FolderTop { get; set;}
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        
    }
}