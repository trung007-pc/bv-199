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
            MeetingContentRepository meetingContentRepository
        )
        {
            _fileFolderRepository = fileFolderRepository;
            _sendingFileRepository = sendingFileRepository;
            _workScheduleRepository = workScheduleRepository;
            _meetingContentRepository = meetingContentRepository;
        }


        public async Task<ChartStatistics> GetChartStatistics(ChartStatisticFilter filter)
        {

            var chart = new ChartStatistics(); 
            //
            // var folders = await _fileFolderRepository.GetMostPopularFolder(filter.FolderTop);
            //
            // foreach (var item in folders)
            // {
            //     chart.FolderItems.Add(new DataItem()
            //     {
            //         Label = item.FileFolder.Name,
            //         Value = item.FileCount
            //     });
            // }
            chart.ReadingRate =await _sendingFileRepository.GetReadingRateOfUser(filter.UserId);

          return chart;
        }

        public async Task<MyWorkStatistics> GetMyWorkStatistics(MyWorkFilter filter)
        {
            var myWorkStatistics = new MyWorkStatistics();
            var meetingContents = await _meetingContentRepository.
                GetListAsync(x => x.IsPublic
                                  && x.CreationTime >= filter.StartDay && x.CreationTime <= filter.EndDay);
            var workSchedules = await _workScheduleRepository
                .GetListAsync(x =>x.CreationTime >= filter.StartDay && x.CreationTime <= filter.EndDay);
            var documentFiles = await _documentFileRepository.GetUnreadDocumentFileOfUser(filter);
            myWorkStatistics.MeetingContents = 
                ObjectMapper.Map<List<MeetingContent>, List<MeetingContentDto>>(meetingContents);
            myWorkStatistics.WorkSchedules = ObjectMapper.Map<List<WorkSchedule>, List<WorkScheduleDto>>(workSchedules);
            myWorkStatistics.DocumentFiles = ObjectMapper.Map<List<DocumentFileWithNavProperties>, List<DocumentFileWithNavPropertiesDto>>(documentFiles);;
            
            return myWorkStatistics;
        }
        
        
    }
}