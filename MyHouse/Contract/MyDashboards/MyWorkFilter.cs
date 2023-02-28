using System;

namespace Contract.MyDashboards
{
    public class MyWorkFilter
    {
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public Guid UserId { get; set; }
    }
}