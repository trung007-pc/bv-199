using System.Collections.Generic;
using Contract.Dashboards;
using Contract.MeetingContents;
using Contract.WorkSchedules;

namespace Contract.MyDashboards
{
    public class GlobalStatistics
    {
        public List<DataItem>  FolderItems { get; set; } = new List<DataItem>();
        public List<WorkScheduleDto> WorkSchedules { get; set; } = new List<WorkScheduleDto>();
        public List<MeetingContentDto> MeetingContents { get; set; } = new List<MeetingContentDto>();
    }
}