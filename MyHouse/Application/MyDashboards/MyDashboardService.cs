using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.WorkSchedules;
using Contract.Dashboards;
using Contract.DocumentFiles;
using Contract.MeetingContents;
using Contract.MyDashboards;
using Contract.WorkSchedules;
using Domain.DocumentFiles;
using Domain.MeetingContents;
using Domain.WorkSchedules;
using SqlServ4r.Repository.FileDocuments;
using SqlServ4r.Repository.FileFolders;
using SqlServ4r.Repository.MeetingContents;
using SqlServ4r.Repository.SendingFiles;
using SqlServ4r.Repository.WorkSchedules;
using Volo.Abp.DependencyInjection;

namespace Application.MyDashboards
{
    public class MyDashboardService : ServiceBase,IMyDashboardService,ITransientDependency
    {
        private readonly FileFolderRepository _fileFolderRepository;
        private readonly SendingFileRepository _sendingFileRepository;
        private readonly WorkScheduleRepository _workScheduleRepository;
        private readonly MeetingContentRepository _meetingContentRepository;
        private readonly DocumentFileRepository _documentFileRepository; 
        public MyDashboardService(
            FileFolderRepository fileFolderRepository,
            SendingFileRepository sendingFileRepository,
            WorkScheduleRepository workScheduleRepository,
            MeetingContentRepository meetingContentRepository,
            DocumentFileRepository documentFileRepository
        )
        {
            _fileFolderRepository = fileFolderRepository;
            _sendingFileRepository = sendingFileRepository;
            _workScheduleRepository = workScheduleRepository;
            _meetingContentRepository = meetingContentRepository;
            _documentFileRepository = documentFileRepository;
        }


        public async Task<GlobalStatistics> GetGlobalStatistics(GlobalStatisticFilter filter)
        {

            var chart = new GlobalStatistics();
            var meetingContents = await _meetingContentRepository.GetListAsync(filter.StartDay, 
                filter.EndDay, true);

            var workSchedules = await _workScheduleRepository.GetListAsync(filter.StartDay, filter.EndDay);
            
             var folders = await _fileFolderRepository.GetMostPopularFolder(filter.FolderTop);
          
            foreach (var item in folders)
            {
                chart.FolderItems.Add(new DataItem()
                {
                    Label = item.FileFolder.Name,
                    Value = item.FileCount,
                    LabelWithValue = $"{item.FileFolder.Name} :{item.FileCount}"
                });
            }
            chart.MeetingContents = ObjectMapper.Map<List<MeetingContent>, List<MeetingContentDto>>(meetingContents);
            chart.WorkSchedules =ObjectMapper.Map<List<WorkSchedule>, List<WorkScheduleDto>>(workSchedules);
          return chart;
        }

        public async Task<MyStatistics> GetMyStatistics(MyStatisticFilter filter)
        {
            var myStatistics = new MyStatistics();

            myStatistics.UnreadDocumentFiles =
                ObjectMapper.Map<List<DocumentFileWithNavProperties>, List<DocumentFileWithNavPropertiesDto>>(await _documentFileRepository.GetUnreadDocumentFileOfUser(filter));

            myStatistics.ReadingRate = await _sendingFileRepository.GetReadingRateOfUser(filter.UserId);
            return myStatistics;
        }   
        
        
    }
}