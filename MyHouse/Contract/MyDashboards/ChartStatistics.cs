using System.Collections.Generic;
using Contract.Dashboards;

namespace Contract.MyDashboards
{
    public class ChartStatistics
    {
        public List<DataItem>  FolderItems { get; set; } = new List<DataItem>();
        
        public double ReadingRate { get; set;}

    }
}