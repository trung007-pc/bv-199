using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorDateRangePicker;
using Contract.Departments;
using Contract.DocumentFiles;
using Contract.FileFolders;
using Contract.FileTypes;
using Contract.Identity.UserManager;
using Contract.IssuingAgencys;
using Contract.SendingFiles;
using Core.Enum;
using Core.Extension;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using WebClient.Components;

namespace WebClient.Pages.Admin
{
    public partial class SharedWithMe
    {
        public List<DocumentFileWithNavPropertiesDto> DocumentFileWithNavProperties { get; set; } = new List<DocumentFileWithNavPropertiesDto>();
        public string HeaderTitle = "Shared With Me";
        
        public List<FileFolderDto> Folders { get; set; }  = new List<FileFolderDto>();
        public List<FileFolderDto> FolderTree { get; set; }  = new List<FileFolderDto>();
        public List<FileFolderDto> HierarchicalFileFolders { get; set; } = new List<FileFolderDto>();
        
        public List<FileTypeDto> FileTypes { get; set; } = new List<FileTypeDto>();
        public List<FileTypeDto> HierarchicalFileTypes { get; set; } = new List<FileTypeDto>();

        public List<IssuingAgencyDto> IssuingAgencies { get; set; } = new List<IssuingAgencyDto>();
        public List<IssuingAgencyDto> HierarchicalIssuingAgencies { get; set; } = new List<IssuingAgencyDto>();
        public FileFolderDto SelectedFolder { get; set; }  = new FileFolderDto();
        public DocumentFileFilter Filter { get; set; } = new DocumentFileFilter();
        
        public RZModel SendingFileModel;
        public IEnumerable<DepartmentDto> HierarchicalDepartments { get; set; } = new List<DepartmentDto>();
        public List<DepartmentDto> Departments { get; set; } = new List<DepartmentDto>();
        public IEnumerable<object> SelectedDepartments = new List<DepartmentDto>();
        public IEnumerable<UserDto> Users = new List<UserDto>();
        public IEnumerable<Guid> SelectedUserIds = new List<Guid>();
        public DocumentFileDto DocumentFileDto { get; set; } = new DocumentFileDto();
        public (DateTimeOffset? StartDay, DateTimeOffset? EndDay) Timeline = (null, null);
        public Dictionary<string, DateRange> DateRanges { get; set; } = new Dictionary<string, DateRange>();

        

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                DateRanges = await GetDateRangePickers();
                await GetDocumentFiles();
                await GetFileFolders();
                await GetDepartments();
                await GetUsers();
                await GetFileTypes();
                await GetIssuingAgencies();
                StateHasChanged();
            }
        }
        
        public async Task GetDocumentFiles()
        {
            Filter.UserId =  await GetUserIdAsync();
            DocumentFileWithNavProperties = await _documentFileService.GetSharedListWithMeAsync(Filter);
        }
        public async Task GetUsers()
        {
            Users = await _userManagerService.GetListAsync();
        }

        public async Task GetFileTypes()
        {
            FileTypes = await _fileTypeService.GetListAsync();
            
            var roots = 
                FileTypes.
                    Where(x => x.ParentCode == null);
            foreach (var item in roots)
            {
                TransformHierarchicalTreeOfTypes(item,0,HierarchicalFileTypes);
            }
            
        }

        public async Task GetIssuingAgencies()
        {
            IssuingAgencies = await _issuingAgencyService.GetListAsync();
            var roots = 
                IssuingAgencies.
                    Where(x => x.ParentCode == null);
            foreach (var item in roots)
            {
                TransformHierarchicalTreeOfAgencies(item,0,HierarchicalIssuingAgencies);
            }
            
        }

        public void TransformHierarchicalTreeOfAgencies(IssuingAgencyDto root,int level ,List<IssuingAgencyDto> hierarchicalIssuingAgencies)
        {
  
            var icon = SetSpaceByLevel(level);
            root.Name = icon + root.Name; 
            hierarchicalIssuingAgencies.Add(root);
            var childNode = IssuingAgencies.
                Where(x => x.ParentCode == root.Id);

            foreach (var item in childNode)
            {
                level++;
                TransformHierarchicalTreeOfAgencies(item,level,hierarchicalIssuingAgencies);
            }
            
        }
        public void TransformHierarchicalTreeOfTypes(FileTypeDto root,int level ,List<FileTypeDto> hierarchicalTypes)
        {
  
            var icon = SetSpaceByLevel(level);
            root.Name = icon + root.Name; 
            hierarchicalTypes.Add(root);
            var childNode = FileTypes.
                Where(x => x.ParentCode == root.Id);

            foreach (var item in childNode)
            {
                level++;
                TransformHierarchicalTreeOfTypes(item,level,hierarchicalTypes);
            }
            
        }
        
        private static string? SetSpaceByLevel(int level)
        {
            var space = "";
            for (int i = 0; i < level; i++)
            {
                space += "-";
            }
            return space;
        }
        
        public async Task OnChangedDate()
        {
            (Filter.StartDay, Filter.EndDay) = GetDateTimeFromOffSet(Timeline.StartDay,Timeline.EndDay);
            await GetDocumentFiles();
        }

        public async Task OnChangeSelectedAgency(object value)
        {
            await GetDocumentFiles();
        }
        
        public async Task OnChangeSelectedType(object value)
        {
            await GetDocumentFiles();
        }
        
        public async Task ViewDocumentFile(DocumentFileWithNavPropertiesDto dto)
        {
            dto.SendingFile.Status = true;
            var sendingFile = ObjectMapper.Map<SendingFileDto,CreateUpdateSendingFileDto>(dto.SendingFile);
            await _sendingFileService.UpdateAsync(sendingFile, dto.SendingFile.Id);
            _navigationManager.NavigateTo($"view-document-file?fileId={dto.File.Id}");
        }
        
        public async Task GetFileFolders()
        {
            Folders =await _fileFolderService.GetListAsync();
            
            foreach (var item in Folders)
            {
                var childFolders = 
                    Folders.Where(x => x.ParentCode == item.Id).ToList();
                item.ChildFolders = childFolders;
            }
            FolderTree = Folders.Where(x => x.ParentCode == null).ToList();
        }

        
        async Task OnChangeSelectedFolder(TreeEventArgs args)
        {
            SelectedFolder = (FileFolderDto) args.Value;
            Filter.DocumentFolderId = SelectedFolder.Id;
            await GetDocumentFiles();
        }

        public async Task GetDepartments()
        {
            Departments = await _departmentService.GetListAsync();
            HierarchicalDepartments = Departments;
            foreach (var item in HierarchicalDepartments)
            {
                var childDepartments = 
                    HierarchicalDepartments.Where(x => x.ParentCode == item.Id).ToList();
                item.ChildDepartment = childDepartments;
            }
            HierarchicalDepartments = HierarchicalDepartments.Where(x => x.ParentCode == null);
            
        }
        
        
        public async Task CreateSendingFiles(Guid fileId)
        {
            await  InvokeAsync(async () =>
            {
                var request = new SendingFileRequest()
                {
                    Sender = await GetUserIdAsync(),
                    DepartmentIds =  (SelectedDepartments.OfType<DepartmentDto>())
                        .Where(x => x.ChildDepartment.Count == 0)
                        .Select(x => x.Id).ToList(),
                    DefineUsers = SelectedUserIds.ToList(),
                    FileId = fileId,
                };

                await _sendingFileService.SendNotificationForDepartmentUsersAndDefineUsers(request);
                
                SendingFileModel.HideModel();
            }, ActionType.Create, true);
        }
        
        async Task OnEnterKeyPressed(KeyboardEventArgs value)
        {
            
            if (value.Key == "Enter")
            {
                await GetDocumentFiles();
            }
        }
        
        public async Task ShowSendingFile(DocumentFileDto dto)
        {
            SelectedDepartments = new List<DepartmentDto>();
            SelectedUserIds = new List<Guid>();
            DocumentFileDto = dto;
            await SendingFileModel.ShowModel();
        }
    }
}