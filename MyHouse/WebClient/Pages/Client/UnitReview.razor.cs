using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Contract.UnitReviewDetails;
using Contract.UnitReviews;
using Contract.Units;
using Contract.Uploads;
using Core.Enum;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;

namespace WebClient.Pages.Client
{
    public partial class UnitReview
    {
        private CreateUpdateUnitReviewDto NewReview { get; set; } = new CreateUpdateUnitReviewDto();
        private Guid EditReviewId { get; set;}
        private List<UnitDto> Units = new List<UnitDto>();
        private Modal CreateModal = new Modal();
        private List<ReviewWithNavPropertiesModel> ReviewsWithNav = new List<ReviewWithNavPropertiesModel>();
        private string ReviewNote { get; set; }
        private List<UnitReviewDto> Result { get; set; }
        
        private IBrowserFile? EnclosedFile { get; set; }
        private string HeaderTitle { get; set; } = "Khảo Sát Hài Lòng Người Bệnh";


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            
            if (firstRender)
            {
                await GetUnits();
                Init();
                StateHasChanged();
                
            }
        }

        public void Init()
        {
            ReviewsWithNav = ObjectMapper.Map<List<UnitDto>, List<ReviewWithNavPropertiesModel>>(Units);
        }

      

        private async Task CreateUnitReviewsWithDetail()
        {

            await InvokeAsync(async () =>
            {
                var details = new List<CreateUpdateUnitReviewDetailDto>();
                foreach (var item in ReviewsWithNav)
                {
                    details.Add(new CreateUpdateUnitReviewDetailDto() {UnitId = item.UnitId, Rate = item.Rating});
                }

                var reviewDto = await _unitReviewService.CreateReviewWithDetailsAsync(details);
                Init();
                EditReviewId = reviewDto.Id;
                await ShowInlineDialog();
            },ActionType.Create,true);

        }

        private async Task UpdatePartReviews()
        {
            if (!ReviewNote.IsNullOrWhiteSpace() || EnclosedFile != null)
            {
                await InvokeAsync(async () =>
                {
                    NewReview.Note = ReviewNote;
                    
                    var fileDto = new FileDto();
                    if (EnclosedFile != null)
                    { 
                        fileDto =  await _uploadService.UploadImage(EnclosedFile);
                        NewReview.ImageUrl = fileDto.Url;
                    }
                    
                    NewReview.FileName = fileDto.FileName;
                    NewReview.Path = fileDto.Path;
                    
                    await _unitReviewService.UpdateAsync(NewReview, EditReviewId);
                    NewReview = new CreateUpdateUnitReviewDto();
                    EditReviewId = new Guid();
                    ReviewNote = "";
                }, ActionType.Update, false);
            }
        }
        
    
        
        private async Task GetUnits()
        {
            Units = await _unitService.GetListAsync(new UnitFilter(){IsActive = true});
        }


        private async Task HideDiaLog(DialogService ds)
        {
            await UpdatePartReviews();
            ds.Close(true);
        }
        
        async Task OnChangeFileAtNewModal(InputFileChangeEventArgs e)
        {
            EnclosedFile = e.GetMultipleFiles().FirstOrDefault();
        }


        public class ReviewWithNavPropertiesModel
        {
            public Guid UnitId { get; set; } = Guid.NewGuid(); 
            public string Name { get; set; }
            public int Rating { get; set;}
            public string ImageUrl { get; set;}
        }
        
    }
}