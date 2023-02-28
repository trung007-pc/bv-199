using System.Collections.Generic;
using Contract.DocumentFiles;
using Contract.MeetingContents;
using Contract.SendingFiles;
using Contract.WorkSchedules;
using Domain.DocumentFiles;
using Domain.MeetingContents;

namespace Contract.MyDashboards
{
    public class MyWorkStatistics
    {
        public List<DocumentFileWithNavPropertiesDto> DocumentFiles = new List<DocumentFileWithNavPropertiesDto>();
        public List<WorkScheduleDto> WorkSchedules = new List<WorkScheduleDto>();
        public List<MeetingContentDto> MeetingContents = new List<MeetingContentDto>();

    }
}