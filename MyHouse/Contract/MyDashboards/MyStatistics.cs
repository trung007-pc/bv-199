using System.Collections.Generic;
using Contract.DocumentFiles;
using Contract.MeetingContents;
using Contract.SendingFiles;
using Contract.WorkSchedules;
using Domain.DocumentFiles;
using Domain.MeetingContents;

namespace Contract.MyDashboards
{
    public class MyStatistics
    {
        public List<DocumentFileWithNavPropertiesDto> UnreadDocumentFiles { get; set; }= new List<DocumentFileWithNavPropertiesDto>();
        public double ReadingRate { get; set;}

    }
}