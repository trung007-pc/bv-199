using System;

namespace Contract.MyDashboards
{
    public class MyStatisticFilter
    {
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public Guid UserId { get; set; }
    }
}