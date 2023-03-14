using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Contract.FileTypes;
using Core.Enum;
using Core.Extension;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebClient.LanguageResources;

namespace WebClient.Pages.Admin
{
    public partial class FileType
    {
        public List<FileTypeDto> HierarchicalTypes = new List<FileTypeDto>();
        public List<FileTypeDto> Types = new List<FileTypeDto>();

        
        public CreateUpdateFileTypeDto NewFileType = new CreateUpdateFileTypeDto();
        public CreateUpdateFileTypeDto EditingFileType = new CreateUpdateFileTypeDto();
        public Guid EditingFileTypeId { get; set; }
         [Inject]  IMessageService _messageService { get; set; }


        public Modal CreateModal;
        public Modal EditingModal;
        public string HeaderTitle = "File Type";

        public IEnumerable<string> Claims = new List<string>();

        public FileTypeDto SelectedFileType = new FileTypeDto();


        public FileType()
        {
            
        }
        
       

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    HeaderTitle = L["FileType"];
                    await GetFileTypes();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }

        public async Task GetFileTypes()
        {
            HierarchicalTypes = await _fileTypeService.GetListAsync();

            Types = HierarchicalTypes.Clone();
            foreach (var item in HierarchicalTypes)
            {
                var childTypes = 
                    HierarchicalTypes.Where(x => x.ParentCode == item.Id).ToList();
                item.ChildTypes = childTypes;
            }

            HierarchicalTypes = HierarchicalTypes.Where(x => x.ParentCode == null).ToList();

        }



        
        

        public async Task CreateFileType()
        {
            await InvokeAsync(async () =>
            {
                await _fileTypeService.CreateAsync(input: NewFileType);
                HideNewModal();
                await GetFileTypes();
            }, ActionType.Create, true);
        }

        public async Task UpdateFileType()
        {
            await InvokeAsync(async () =>
            {
                await _fileTypeService.UpdateAsync(EditingFileType, EditingFileTypeId);
                HideEditModal();    
                await GetFileTypes();
            },ActionType.Update,true);
            
        }

        public async Task DeleteFileType(Guid id)
        {
            await InvokeAsync(async () =>
            {
                await _fileTypeService.DeleteAsync(id);
                HideEditModal();
                await GetFileTypes();
            },ActionType.Delete,true);
            
        }

        public async Task ShowConfirmMessage(object value)
        {
            var department = (FileTypeDto)value;

            if (await _messageService.Confirm(L["Confirmation.Message"], L["Confirmation"]))
            {
                await DeleteFileType(department.Id);
            }
        }

        public void ShowNewModal()
        {
            NewFileType = new CreateUpdateFileTypeDto();
            CreateModal.Show();
        }

        public void HideNewModal()
        {
            CreateModal.Hide();
        }

        public Task ShowEditingModal(object value)
        {
            var department = (FileTypeDto)value;

            EditingFileType = new CreateUpdateFileTypeDto();
            EditingFileType = ObjectMapper.Map<FileTypeDto, CreateUpdateFileTypeDto>(department);
            EditingFileTypeId = department.Id;
            return EditingModal.Show();
        }

        public void HideEditModal()
        {
            EditingModal.Hide();
        }
        
        

        void OnChangeNewFileType(TreeEventArgs args)
        {
            var department = (FileTypeDto) args.Value;
            NewFileType.ParentCode = department.Id;
        }
       
        void OnChangeUpdateFileType(TreeEventArgs args)
        {
            
            var department = (FileTypeDto) args.Value;
            
            if (EditingFileTypeId != department.Id)
            {
                if (IsChildOfAssignmentFileType(EditingFileTypeId,department))
                {
                    NotifyMessage(NotificationSeverity.Warning, "You can't choose its child",4000);
                }
                else
                {
                    EditingFileType.ParentCode = department.Id;
                }
            }
        }


        bool IsChildOfAssignmentFileType(Guid assignmentId,FileTypeDto dto)
        {
            var childFileTypes = new List<FileTypeDto>();
            var department = Types.FirstOrDefault(x => x.Id == assignmentId);
            GetAllChildOfFileType(childFileTypes, department);

            var item = childFileTypes.FirstOrDefault(x => x.Id == dto.Id);

            if (item != null) return true;

            return false;

        }       

        void GetAllChildOfFileType(List<FileTypeDto> childs,FileTypeDto dto)
        {
            var items = Types.Where(x => x.ParentCode == dto.Id);
            childs.AddRange(items);
            foreach (var item in items)
            {
                GetAllChildOfFileType(childs, item);
            }
            
        }
    }
}