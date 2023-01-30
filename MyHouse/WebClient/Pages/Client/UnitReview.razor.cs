using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Contract.UnitReviewDetails;
using Contract.UnitReviews;
using Contract.Units;
using Contract.UnitTypes;
using Contract.Uploads;
using Core.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Radzen;

namespace WebClient.Pages.Client
{
    public partial class UnitReview 
    {
        public CreateUpdateUnitReviewDto NewReview { get; set; } = new CreateUpdateUnitReviewDto();
        public Guid EditReviewId { get; set;}
        public List<UnitWithNavPropertiesDto> Units = new List<UnitWithNavPropertiesDto>();
        public Modal CreateModal = new Modal();
        public List<ReviewWithNavPropertiesModel> ReviewsWithNav = new List<ReviewWithNavPropertiesModel>();
        public string ReviewNote { get; set; }
        public List<UnitReviewDto> Result { get; set; }
        
        public IBrowserFile? EnclosedFile { get; set; }
        public string HeaderTitle { get; set; } = "Khảo Sát Hài Lòng Người Bệnh";

        [Parameter]
        public Guid? TypeId { get; set; }

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
            ReviewsWithNav = ObjectMapper.Map<List<UnitWithNavPropertiesDto>, List<ReviewWithNavPropertiesModel>>(Units);
        }

      

        public async Task CreateUnitReviewsWithDetail()
        {

            await InvokeAsync(async () =>
            {
                var details = new List<CreateUpdateUnitReviewDetailDto>();
                foreach (var item in ReviewsWithNav)
                {
                    details.Add(new CreateUpdateUnitReviewDetailDto() {UnitId = item.Unit.Id, Rate = item.Rating});
                }

                var reviewDto = await _unitReviewService.CreateReviewWithDetailsAsync(details);
                Init();
                EditReviewId = reviewDto.Id;
                await ShowInlineDialog();
            },ActionType.Create,true);

        }

        public async Task UpdatePartReviews()
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
        
    
        
        public async Task GetUnits()
        {
             Units = await _unitService.GetListWithNavPropertiesAsync(new UnitFilter(){IsActive = true,UnitTypeId = TypeId});
        }


        public async Task HideDiaLog(DialogService ds)
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
            public UnitDto Unit { get; set; }
        
            public UnitTypeDto UnitType { get; set;}
            public int Rating { get; set;}
        }
        
    }
}