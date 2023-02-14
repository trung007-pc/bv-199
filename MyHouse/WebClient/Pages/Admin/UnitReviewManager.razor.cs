using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using BlazorDateRangePicker;
using Blazorise;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Contract.UnitReviewDetails;
using Contract.UnitReviews;
using Contract.Units;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebClient.Helper;
using WebClient.Shared;

namespace WebClient.Pages.Admin
{
    public partial class UnitReviewManager
    {
        [Inject] IMessageService _messageService { get; set; }
        public List<UnitReviewDto> UnitReviews { get; set; } =
            new List<UnitReviewDto>();

        public Modal ViewDetailModal = new Modal();

        public DefaultModal DefaultModal = new DefaultModal();

        public string HeaderTitle { get; set; } = "Unit Review";
        
        public List<UnitReviewDetailDto> Details = new List<UnitReviewDetailDto>();

        public Dictionary<string, DateRange> DateRanges { get; set; } = new Dictionary<string, DateRange>();
        public (DateTimeOffset? StartDay, DateTimeOffset? EndDay) Timeline = (null, null);

        public UnitReviewFilter Filter { get; set; } = new UnitReviewFilter();


        public UnitReviewManager()
        {
            
        }


        protected override async Task OnInitializedAsync()
        {
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    DateRanges = await GetDateRangePickers();
                    await GetUnitReviews();
                    StateHasChanged();
                }, ActionType.GetList, false);
         
            }
        }

        public async Task GetUnitReviews()
        {
            UnitReviews = await _unitReviewService.GetListWithCalculatingAverageAsync(Filter);
            UnitReviews = UnitReviews.OrderByDescending(x => x.CreationDate).ToList();
        }
        
        public async Task ShowConfirmMessage(Guid reviewId)
        {
            if ( await _messageService.Confirm( "Are you sure you want to confirm?", "Confirmation" ) )
            {
                await InvokeAsync(async () =>
                {
                    await DeleteUnitReview(reviewId);
                }, ActionType.Delete, true);
            }
            
        }
        public async Task DeleteUnitReview(Guid reviewId)
        {
            await _unitReviewService.DeleteAsync(reviewId);
            await GetUnitReviews();
        }
        

        public async Task ShowViewDetailModal(Guid reviewId)
        {
            DefaultModal = new DefaultModal();
            Details = await _unitReviewDetailService.GetDetailsByReviewIdAsync(reviewId);
            await ViewDetailModal.Show();
        }
        public void HideViewDetailModal()
        {
            ViewDetailModal.Hide();
        }
        
        public async Task OnChangedDate()
        {
            (Filter.StartDay, Filter.EndDay) = GetDateTimeFromOffSet(Timeline.StartDay,Timeline.EndDay);
            await GetUnitReviews();
        }
        
        public async Task DownloadExcelReport()
        {
            var rows = new List<UnitReviewRow>();

            foreach (var item in UnitReviews)
            {
                rows.Add(new UnitReviewRow()
                {
                    Note = item.Note,
                    CreationDate = item.CreationDate.ToString(),
                    AveragePoint = item.AveragePoint.ToString(),
                    ImageUrl = item.ImageUrl
                });
            }
            
            var bytes =  ExportHelper.GeneratePostsExcelBytes(rows);
            await _downloadFile.DownloadFileAsync(bytes,"xlsx","unit-review-report");
        }
        
    }

    public class UnitReviewRow
    {
        public string CreationDate { get; set; }
        public string AveragePoint { get; set;}
        public string? ImageUrl { get; set; }
        public string Note { get; set; }
    }
}