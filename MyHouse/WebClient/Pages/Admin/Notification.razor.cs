using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.Notifications;

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
                Notifications = await _notificationService.GetListByFilter(Filter);
                StateHasChanged();
            }
        }
    }
}