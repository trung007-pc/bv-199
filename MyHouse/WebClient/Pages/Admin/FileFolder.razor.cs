using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Contract.FileFolders;
using Core.Enum;
using Core.Extension;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebClient.LanguageResources;

namespace WebClient.Pages.Admin
{
    public partial class FileFolder
    {
        public List<FileFolderDto> HierarchicalFolders = new List<FileFolderDto>();
        public List<FileFolderDto> Folder = new List<FileFolderDto>();

        
        public CreateUpdateFileFolderDto NewFileFolder = new CreateUpdateFileFolderDto();
        public CreateUpdateFileFolderDto EditingFileFolder = new CreateUpdateFileFolderDto();
        public Guid EditingFileFolderId { get; set; }
         [Inject]  IMessageService _messageService { get; set; }


        public Modal CreateModal;
        public Modal EditModal;
        public string HeaderTitle = "File Folder";




        public FileFolder()
        {
            
        }
        
       

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    HeaderTitle = L["Folder"];
                    await GetFileFolders();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }

        public async Task GetFileFolders()
        {
            HierarchicalFolders = await _fileFolderService.GetListAsync();

            Folder = HierarchicalFolders.Clone();
            foreach (var item in HierarchicalFolders)
            {
                var childFolders = 
                    HierarchicalFolders.Where(x => x.ParentCode == item.Id).ToList();
                item.ChildFolders = childFolders;
            }

            HierarchicalFolders = HierarchicalFolders.Where(x => x.ParentCode == null).ToList();

        }



        
        

        public async Task CreateFileFolder()
        {
            await InvokeAsync(async () =>
            {
                await _fileFolderService.CreateAsync(input: NewFileFolder);
                HideNewModal();
                await GetFileFolders();
            }, ActionType.Create, true);
        }

        public async Task UpdateFileFolder()
        {
            await InvokeAsync(async () =>
            {
                await _fileFolderService.UpdateAsync(EditingFileFolder, EditingFileFolderId);
                HideEditModal();    
                await GetFileFolders();
            },ActionType.Update,true);
            
        }

        public async Task DeleteFileFolder(Guid id)
        {
            await InvokeAsync(async () =>
            {
                await _fileFolderService.DeleteAsync(id);
                HideEditModal();
                await GetFileFolders();
            },ActionType.Delete,true);
            
        }

        public async Task ShowConfirmMessage(object value)
        {
            var department = (FileFolderDto)value;

            if (await _messageService.Confirm(L["Confirmation.Message"], L["Confirmation"]))
            {
                await DeleteFileFolder(department.Id);
            }
        }

        public void ShowNewModal()
        {
            NewFileFolder = new CreateUpdateFileFolderDto();
            CreateModal.Show();
        }

        public void HideNewModal()
        {
            CreateModal.Hide();
        }

        public Task ShowEditingModal(object value)
        {
            var department = (FileFolderDto)value;

            EditingFileFolder = new CreateUpdateFileFolderDto();
            EditingFileFolder = ObjectMapper.Map<FileFolderDto, CreateUpdateFileFolderDto>(department);
            EditingFileFolderId = department.Id;
            return EditModal.Show();
        }

        public void HideEditModal()
        {
            EditModal.Hide();
        }
        
        

        void OnChangeNewFileFolder(TreeEventArgs args)
        {
            var department = (FileFolderDto) args.Value;
            NewFileFolder.ParentCode = department.Id;
        }
       
        void OnChangeUpdateFileFolder(TreeEventArgs args)
        {
            
            var department = (FileFolderDto) args.Value;
            
            if (EditingFileFolderId != department.Id)
            {
                if (IsChildOfAssignmentFileFolder(EditingFileFolderId,department))
                {
                    NotifyMessage(NotificationSeverity.Warning, "You can't choose its child",4000);
                }
                else
                {
                    EditingFileFolder.ParentCode = department.Id;
                }
            }
        }


        bool IsChildOfAssignmentFileFolder(Guid assignmentId,FileFolderDto dto)
        {
            var childFileFolders = new List<FileFolderDto>();
            var department = Folder.FirstOrDefault(x => x.Id == assignmentId);
            GetAllChildOfFileFolder(childFileFolders, department);

            var item = childFileFolders.FirstOrDefault(x => x.Id == dto.Id);

            if (item != null) return true;

            return false;

        }       

        void GetAllChildOfFileFolder(List<FileFolderDto> childs,FileFolderDto dto)
        {
            var items = Folder.Where(x => x.ParentCode == dto.Id);
            childs.AddRange(items);
            foreach (var item in items)
            {
                GetAllChildOfFileFolder(childs, item);
            }
            
        }
    }
}