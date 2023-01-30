using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using Blazorise;
using Contract.Identity.RoleManager;
using Contract.Identity.UserManager;
using Contract.UnitReviews;
using Contract.Units;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace WebClient.Pages.Admin
{
    public partial class UnitReviewManager
    {
        [Inject] IMessageService _messageService { get; set; }
        public List<UnitReviewDto> UnitReviews { get; set; } =
            new List<UnitReviewDto>();

        public CreateUpdateUnitReviewDto NewReview { get; set; } = new CreateUpdateUnitReviewDto();
        public CreateUpdateUnitReviewDto EditReview { get; set; } = new CreateUpdateUnitReviewDto();
        public List<UnitDto> Units = new List<UnitDto>();
       


        public Guid SelectedNewUnitId = new Guid();
        public Guid EditUnitId { get; set; }
        public Guid EditReviewId { get; set;}

        public Modal CreateModal = new Modal();
        
        public Modal EditModal = new Modal();
        public string HeaderTitle { get; set; } = "Unit Review";


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
                    await GetUnitReviews();
                    StateHasChanged();
                }, ActionType.GetList, false);
         
            }
        }

        public async Task GetUnitReviews()
        {
            UnitReviews = await _unitReviewService.GetListWithCalculatingAverageAsync();
        }
        public async Task NavigateToDetails(Guid reviewId)
        {
            _navigationManager.NavigateTo($"unit-review-detail?reviewId={reviewId}");
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
        
        public void ShowNewModal()
        {
            NewReview = new CreateUpdateUnitReviewDto();
            CreateModal.Show();
        }
        
        
        public void ShowEditModal(UnitReviewDto input)
        {
            EditUnitId = input.Id;
            EditReviewId = input.Id;
            EditModal.Show();
        }
    }
}