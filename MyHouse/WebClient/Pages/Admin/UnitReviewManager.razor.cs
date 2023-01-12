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
        private List<UnitReviewDto> UnitReviews { get; set; } =
            new List<UnitReviewDto>();

        private CreateUpdateUnitReviewDto NewReview { get; set; } = new CreateUpdateUnitReviewDto();
        private CreateUpdateUnitReviewDto EditReview { get; set; } = new CreateUpdateUnitReviewDto();
        private List<UnitDto> Units = new List<UnitDto>();
       


        private Guid SelectedNewUnitId = new Guid();
        private Guid EditUnitId { get; set; }
        private Guid EditReviewId { get; set;}

        private Modal CreateModal = new Modal();
        
        private Modal EditModal = new Modal();
        private string HeaderTitle { get; set; } = "Unit Review";


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
                await GetUnitReviews();
                StateHasChanged();
            }
        }

        private async Task GetUnitReviews()
        {
            UnitReviews = await _unitReviewService.GetListWithCalculatingAverageAsync();
        }
        private async Task NavigateToDetails(Guid reviewId)
        {
            _navigationManager.NavigateTo($"unit-review-detail?reviewId={reviewId}");
        }

        private async Task ShowConfirmMessage(Guid reviewId)
        {
            if ( await _messageService.Confirm( "Are you sure you want to confirm?", "Confirmation" ) )
            {
                await InvokeAsync(async () =>
                {
                    await DeleteUnitReview(reviewId);
                }, ActionType.Delete, true);
            }
            
        }
        private async Task DeleteUnitReview(Guid reviewId)
        {
            await _unitReviewService.DeleteAsync(reviewId);
            await GetUnitReviews();
        }
        
        private void ShowNewModal()
        {
            NewReview = new CreateUpdateUnitReviewDto();
            CreateModal.Show();
        }
        
        
        private void ShowEditModal(UnitReviewDto input)
        {
            EditUnitId = input.Id;
            EditReviewId = input.Id;
            EditModal.Show();
        }
    }
}