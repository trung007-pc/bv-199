using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Contract.Departments;
using Core.Enum;
using Core.Extension;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace WebClient.Pages.Admin
{
    public partial class Department
    {
        public List<DepartmentDto> Departments = new List<DepartmentDto>();
        public List<DepartmentDto> AllDepartmentElements = new List<DepartmentDto>();

        
        public CreateUpdateDepartmentDto NewDepartment = new CreateUpdateDepartmentDto();
        public CreateUpdateDepartmentDto EditDepartment = new CreateUpdateDepartmentDto();
        public Guid EditingDepartmentId { get; set; }
         [Inject]  IMessageService _messageService { get; set; }


        public Modal CreateModal;
        public Modal EditModal;
        public string HeaderTitle = "Department";

        public IEnumerable<string> Claims = new List<string>();

        public DepartmentDto SelectedDepartment = new DepartmentDto();



        public Department()
        {
            
        }
        
       

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    await GetDepartments();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }

        public async Task GetDepartments()
        {
            Departments = await _departmentService.GetListAsync();

            AllDepartmentElements = Departments.Clone();
            foreach (var item in Departments)
            {
                var childDepartments = 
                    Departments.Where(x => x.ParentCode == item.Id).ToList();
                item.ChildDepartment = childDepartments;
            }

            Departments = Departments.Where(x => x.ParentCode == null).ToList();

        }



        
        

        public async Task CreateDepartment()
        {
            await InvokeAsync(async () =>
            {
                await _departmentService.CreateAsync(input: NewDepartment);
                HideNewModal();
                await GetDepartments();
            }, ActionType.Create, true);
        }

        public async Task UpdateDepartment()
        {
            await InvokeAsync(async () =>
            {
                await _departmentService.UpdateAsync(EditDepartment, EditingDepartmentId);
                HideEditModal();    
                await GetDepartments();
            },ActionType.Update,true);
            
        }

        public async Task DeleteDepartment(Guid id)
        {
            await InvokeAsync(async () =>
            {
                await _departmentService.DeleteAsync(id);
                HideEditModal();
                await GetDepartments();
            },ActionType.Delete,true);
            
        }

        public async Task ShowConfirmMessage(object value)
        {
            var department = (DepartmentDto)value;

            if (await _messageService.Confirm("Are you sure you want to confirm?", "Confirmation"))
            {
                await DeleteDepartment(department.Id);
            }
        }

        public void ShowNewModal()
        {
            NewDepartment = new CreateUpdateDepartmentDto();
            CreateModal.Show();
        }

        public void HideNewModal()
        {
            CreateModal.Hide();
        }

        public Task ShowEditModal(object value)
        {
            var department = (DepartmentDto)value;

            EditDepartment = new CreateUpdateDepartmentDto();
            EditDepartment = ObjectMapper.Map<DepartmentDto, CreateUpdateDepartmentDto>(department);
            EditingDepartmentId = department.Id;
            return EditModal.Show();
        }

        public void HideEditModal()
        {
            EditModal.Hide();
        }
        
        

        void OnChangeNewDepartment(TreeEventArgs args)
        {
            var department = (DepartmentDto) args.Value;
            NewDepartment.ParentCode = department.Id;
        }
       
        void OnChangeUpdateDepartment(TreeEventArgs args)
        {
            
            var department = (DepartmentDto) args.Value;
            
            if (EditingDepartmentId != department.Id)
            {
                if (IsChildOfAssignmentDepartment(EditingDepartmentId,department))
                {
                    NotifyMessage(NotificationSeverity.Warning, "You can't choose its child",4000);
                }
                else
                {
                    EditDepartment.ParentCode = department.Id;
                }
            }
        }


        bool IsChildOfAssignmentDepartment(Guid assignmentId,DepartmentDto dto)
        {
            var childDepartments = new List<DepartmentDto>();
            var department = AllDepartmentElements.FirstOrDefault(x => x.Id == assignmentId);
            GetAllChildOfDepartment(childDepartments, department);

            var item = childDepartments.FirstOrDefault(x => x.Id == dto.Id);

            if (item != null) return true;

            return false;

        }       

        void GetAllChildOfDepartment(List<DepartmentDto> childs,DepartmentDto dto)
        {
            var items = AllDepartmentElements.Where(x => x.ParentCode == dto.Id);
            childs.AddRange(items);
            foreach (var item in items)
            {
                GetAllChildOfDepartment(childs, item);
            }
            
        }
        
        
        
        
        
        
        
        
   
       
    }

    public class DepartmentParent
    {
        public Guid ParentCode { get; set;}
        public string Name { get; set; }
    }

 


    

}