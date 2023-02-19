using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Contract.IssuingAgencys;
using Core.Enum;
using Core.Extension;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace WebClient.Pages.Admin
{
    public partial class IssuingAgency
    {
        public List<IssuingAgencyDto> HierarchicalAgencies = new List<IssuingAgencyDto>();
        public List<IssuingAgencyDto> Agencies = new List<IssuingAgencyDto>();

        
        public CreateUpdateIssuingAgencyDto NewIssuingAgency = new CreateUpdateIssuingAgencyDto();
        public CreateUpdateIssuingAgencyDto EditingIssuingAgency = new CreateUpdateIssuingAgencyDto();
        public Guid EditingIssuingAgencyId { get; set; }
         [Inject]  IMessageService _messageService { get; set; }


        public Modal CreateModal;
        public Modal EditModal;
        public string HeaderTitle = "Issuing Agency";





        public IssuingAgency()
        {
            
        }
        
       

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    await GetIssuingAgencies();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }

        public async Task GetIssuingAgencies()
        {
            HierarchicalAgencies = await _issuingAgencyService.GetListAsync();

            Agencies = HierarchicalAgencies.Clone();
            foreach (var item in HierarchicalAgencies)
            {
                var childAgencys = 
                    HierarchicalAgencies.Where(x => x.ParentCode == item.Id).ToList();
                item.ChildAgencies = childAgencys;
            }

            HierarchicalAgencies = HierarchicalAgencies.Where(x => x.ParentCode == null).ToList();

        }



        
        

        public async Task CreateIssuingAgency()
        {
            await InvokeAsync(async () =>
            {
                await _issuingAgencyService.CreateAsync(input: NewIssuingAgency);
                HideNewModal();
                await GetIssuingAgencies();
            }, ActionType.Create, true);
        }

        public async Task UpdateIssuingAgency()
        {
            await InvokeAsync(async () =>
            {
                await _issuingAgencyService.UpdateAsync(EditingIssuingAgency, EditingIssuingAgencyId);
                HideEditModal();    
                await GetIssuingAgencies();
            },ActionType.Update,true);
            
        }

        public async Task DeleteIssuingAgency(Guid id)
        {
            await InvokeAsync(async () =>
            {
                await _issuingAgencyService.DeleteAsync(id);
                HideEditModal();
                await GetIssuingAgencies();
            },ActionType.Delete,true);
            
        }

        public async Task ShowConfirmMessage(object value)
        {
            var department = (IssuingAgencyDto)value;

            if (await _messageService.Confirm("Are you sure you want to confirm?", "Confirmation"))
            {
                await DeleteIssuingAgency(department.Id);
            }
        }

        public void ShowNewModal()
        {
            NewIssuingAgency = new CreateUpdateIssuingAgencyDto();
            CreateModal.Show();
        }

        public void HideNewModal()
        {
            CreateModal.Hide();
        }

        public Task ShowEditingModal(object value)
        {
            var department = (IssuingAgencyDto)value;

            EditingIssuingAgency = new CreateUpdateIssuingAgencyDto();
            EditingIssuingAgency = ObjectMapper.Map<IssuingAgencyDto, CreateUpdateIssuingAgencyDto>(department);
            EditingIssuingAgencyId = department.Id;
            return EditModal.Show();
        }

        public void HideEditModal()
        {
            EditModal.Hide();
        }
        
        

        void OnChangeNewIssuingAgency(TreeEventArgs args)
        {
            var department = (IssuingAgencyDto) args.Value;
            NewIssuingAgency.ParentCode = department.Id;
        }
       
        void OnChangeUpdateIssuingAgency(TreeEventArgs args)
        {
            
            var department = (IssuingAgencyDto) args.Value;
            
            if (EditingIssuingAgencyId != department.Id)
            {
                if (IsChildOfAssignmentIssuingAgency(EditingIssuingAgencyId,department))
                {
                    NotifyMessage(NotificationSeverity.Warning, "You can't choose its child",4000);
                }
                else
                {
                    EditingIssuingAgency.ParentCode = department.Id;
                }
            }
        }


        bool IsChildOfAssignmentIssuingAgency(Guid assignmentId,IssuingAgencyDto dto)
        {
            var childIssuingAgencys = new List<IssuingAgencyDto>();
            var department = Agencies.FirstOrDefault(x => x.Id == assignmentId);
            GetAllChildOfIssuingAgency(childIssuingAgencys, department);

            var item = childIssuingAgencys.FirstOrDefault(x => x.Id == dto.Id);

            if (item != null) return true;

            return false;

        }       

        void GetAllChildOfIssuingAgency(List<IssuingAgencyDto> childs,IssuingAgencyDto dto)
        {
            var items = Agencies.Where(x => x.ParentCode == dto.Id);
            childs.AddRange(items);
            foreach (var item in items)
            {
                GetAllChildOfIssuingAgency(childs, item);
            }
            
        }
    }
}