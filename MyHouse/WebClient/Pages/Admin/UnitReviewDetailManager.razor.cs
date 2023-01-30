using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Contract.UnitReviewDetails;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace WebClient.Pages.Admin
{
    public partial class UnitReviewDetailManager
    {
        
        [Parameter]
        [SupplyParameterFromQuery(Name = "reviewId")]
        public Guid ReviewID { get; set; }



        public List<UnitReviewDetailDto> Details = new List<UnitReviewDetailDto>();
        public CreateUpdateUnitReviewDetailDto NewDetail = new CreateUpdateUnitReviewDetailDto();
        public CreateUpdateUnitReviewDetailDto EditDetail = new CreateUpdateUnitReviewDetailDto();
        public Guid EditDetailId { get; set;}
        
        public Modal CreateModal;
        public Modal EditModal;
        public string HeaderTitle { get; set; } = "Review Detail ";



        protected override Task OnInitializedAsync()
        {
            
            return base.OnInitializedAsync();
        }


        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    await GetDetails();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }

        public async Task CreateDetail()
        {
            await InvokeAsync(async () =>
            {
                NewDetail.UnitReviewId = ReviewID;
                await _unitReviewDetailService.CreateAsync(NewDetail);
                await GetDetails();
                HideNewModal();
            },ActionType.Create,true);
        }
        
        public async Task UpdateDetail()
        {

            await InvokeAsync(async () =>
            {
                await _unitReviewDetailService.UpdateAsync(EditDetail, EditDetailId);
                await GetDetails();
                HideEditModal();
            }, ActionType.Update, true);
        }

        public async Task DeleteDetail(Guid id)
        {
            await InvokeAsync( async () =>
            {
                await _unitReviewDetailService.DeleteAsync(id);
                await GetDetails();
            }, ActionType.Delete, true);

        }
        
        public async Task GetDetails()
        {
            Details = await _unitReviewDetailService.GetDetailsByReviewIdAsync(ReviewID);
        }
        
        public void ShowNewModal()
        {
            NewDetail = new CreateUpdateUnitReviewDetailDto();
            CreateModal.Show();
        }
        

        public void HideNewModal()
        {
            NewDetail = new CreateUpdateUnitReviewDetailDto();
            CreateModal.Hide();
        }
        
        
        
        public Task ShowEditModal(UnitReviewDetailDto input)
        {
            EditDetail = new CreateUpdateUnitReviewDetailDto();
            EditDetailId = input.Id;
            EditDetail = ObjectMapper.Map<UnitReviewDetailDto,CreateUpdateUnitReviewDetailDto>(input);
            return EditModal.Show();
        }

        public  void HideEditModal()
        {
             EditModal.Hide();
        }
        
        

        
    }
}