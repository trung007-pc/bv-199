using System;
using Core.Enum;

namespace Contract.WorkSchedules
{
    public class WorkScheduleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Node { get; set; }
        public DateTime CreationTime { get; set; } =DateTime.Now;
        public DateTime StartDay { get; set; } =DateTime.Now;
        public DateTime EndDay { get; set; } =DateTime.Now;
        public ScheduleStatus ScheduleStatus { get; set; } = ScheduleStatus.Cancel;
        public Guid CreatedBy { get; set; }

        public string? FileName { get; set; }
        public string? Path { get; set;}
        public string? Url { get; set;}
    }
}