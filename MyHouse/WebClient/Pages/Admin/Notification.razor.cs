using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.Notifications;
using Core.Enum;

namespace WebClient.Pages.Admin
{
    public partial class Notification
    {
        public string HeaderTitle = "Notifications";
        public List<NotificationDto> Notifications { get; set; } = new List<NotificationDto>();

        public NotificationFilter Filter { get; set; } = new NotificationFilter();

        protected override void OnInitialized()
        {
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Filter.UserName = await GetUserNameAsync();
                Filter.Status = NotificationStatus.All;
                await GetNotifications();
                StateHasChanged();
            }
        }

        public async Task GetNotifications()
        {
            Notifications = await _notificationService.GetListByFilter(Filter);
            foreach (var item in Notifications.Where(x=>!x.Status))
            {
                item.Visible = true;
            }
        }
        

        public void OnSelectAll(bool value)
        {
            if (value)
            {
                foreach (var item in Notifications.Where(x=>x.Visible))
                {
                    item.Status = true;
                }
            }
            else
            {
                foreach (var item in Notifications.Where(x=>x.Visible))
                {
                    item.Status = false;
                }
            }
        }

        public async Task ConfirmRead()
        {

            await InvokeAsync(async () =>
            {
                var notifications = Notifications.Where(x => x.Status && x.Visible).ToList();
                var updateNotifications =
                    ObjectMapper.Map<List<NotificationDto>, List<UpdateNotification>>(notifications);
                await _notificationService.UpdateListWithReadingStatusAsync(updateNotifications);
                await GetNotifications();
            },ActionType.Update,true);

        }

        public async Task ViewNotification(NotificationDto dto)
        {
            if (dto.Type == NotificationType.Document)
            {
                if (!dto.Status)
                {
                    await InvokeAsync(async () =>
                    {
                        var updateNotifications =
                            ObjectMapper.Map<NotificationDto, UpdateNotification>(dto);
                        updateNotifications.Status = true;
                        await _notificationService.UpdateWithReadingStatusAsync(updateNotifications);

                    },ActionType.Update,false);
                }
                var sendingFile =  await _sendingFileService.GetAsync(dto.DestinationCode);
                _navigationManager.NavigateTo($"view-document-file?fileId={sendingFile.FileId}",true);
            }
        }

    }
}