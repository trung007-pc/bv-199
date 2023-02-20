﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using BlazorDateRangePicker;
using Blazorise;
using Contract.DocumentFiles;
using Contract.FileFolders;
using Contract.FileTypes;
using Contract.IssuingAgencys;
using Core.Enum;
using Core.Extension;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using Radzen;
using WebClient.Exceptions;
using WebClient.Setting;

namespace WebClient.Pages.Admin
{
    
    public partial class DocumentFile
    {
  
        public List<DocumentFileWithNavPropertiesDto> DocumentFileWithNavProperties { get; set; } = new List<DocumentFileWithNavPropertiesDto>();
        public List<FileTypeDto> FileTypes { get; set; } = new List<FileTypeDto>();
        public List<FileTypeDto> HierarchicalFileTypes { get; set; } = new List<FileTypeDto>();

        public List<IssuingAgencyDto> IssuingAgencies { get; set; } = new List<IssuingAgencyDto>();
        public List<IssuingAgencyDto> HierarchicalIssuingAgencies { get; set; } = new List<IssuingAgencyDto>();
        

        public List<FileFolderDto> FolderTree { get; set; }  = new List<FileFolderDto>();
        public List<FileFolderDto> HierarchicalFileFolders { get; set; } = new List<FileFolderDto>();


        public CreateUpdateDocumentFileDto NewDocumentFile { get; set; } = new CreateUpdateDocumentFileDto();
        public CreateUpdateDocumentFileDto EditingDocumentFile { get; set; } = new CreateUpdateDocumentFileDto();
        public DocumentFileDto ViewDocumentFile { get; set; } = new DocumentFileDto();

        public FileFolderDto SelectedFolder { get; set; }  = new FileFolderDto();
        public List<FileFolderDto> Folders { get; set; }  = new List<FileFolderDto>();
        public Dictionary<string, DateRange> DateRanges { get; set; } = new Dictionary<string, DateRange>();
        public (DateTimeOffset? StartDay, DateTimeOffset? EndDay) Timeline = (null, null);
        public Guid EditingDocumentFileId { get; set; }
         [Inject]  IMessageService _messageService { get; set; }

         public DocumentFileFilter Filter { get; set; } = new DocumentFileFilter();

        public Modal CreateModal;
        public Modal EditingModal;
        public Modal ViewModal;

        public string HeaderTitle = "Document File";

        public IBrowserFile? NewFile { get; set; }
        
        public IBrowserFile? PdfFile { get; set; }

        public IBrowserFile? EditingFile { get; set; }

        private bool Test1 { get; set; }


        bool sidebar1Expanded = true;
        bool sidebar2Expanded = true;
        bool sidebar3Expanded = true;
        bool sidebar4Expanded = true;
        bool sidebar5Expanded = true;
        bool sidebar6Expanded = true;

        public DocumentFile()
        {
        }
        
       

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await InvokeAsync(async () =>
                {
                    DateRanges = await GetDateRangePickers();
                    await GetDocumentFiles();
                    await GetFileTypes();
                    await GetIssuingAgencies();
                    await GetFileFolders();
                    StateHasChanged();
                }, ActionType.GetList, false);
            }
        }

        public async Task GetDocumentFiles()
        {
            DocumentFileWithNavProperties = await _documentFileService.GetListWithNavPropertiesAsync(Filter);
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


            var folders = Folders.CloneList();
            
            var roots = 
                folders.Where(x => x.ParentCode == null).ToList().CloneList();


            
            foreach (var item in roots)
            {
                TransformHierarchicalTreeOfFolder(item,folders,0,HierarchicalFileFolders);
            }
            
        }

        
        public void TransformHierarchicalTreeOfFolder(FileFolderDto root,  List<FileFolderDto> folders,
            int level ,
            List<FileFolderDto> hierarchicalFolders)
        {
            
            var icon = SetSpaceByLevel(level);
            root.Name = icon + root.Name; 
            hierarchicalFolders.Add(root);
            var childNode = folders.
                Where(x => x.ParentCode == root.Id);

            foreach (var item in childNode)
            {
                level++;
                TransformHierarchicalTreeOfFolder(item,folders,level,hierarchicalFolders);
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
        
        public byte[] FileBytes { get; set; }
        async  Task OnChangeFileAtNewModal(InputFileChangeEventArgs e)
        {

            await InvokeAsync(async () =>
            {
                NewFile = null;
                NewFile = e.File;
                 
                if (e.File.Size > BlazorSetting.Document_FILE_LENGTH_LIMIT)
                    throw new FailedOperation("File size is too big. please choose file have less 10Mb");
                
                if (NewFile.ContentType.Contains("pdf"))
                {
                    PdfFile = NewFile;
                }
               

              
            }, ActionType.UploadFile);

       
        }
        
        
        async Task OnChangeFileAtEditingModal(InputFileChangeEventArgs e)
        {
            await InvokeAsync(async () =>
            {
                EditingFile = null;
                EditingFile = e.File;
                if (e.File.Size > BlazorSetting.Document_FILE_LENGTH_LIMIT)
                    throw new FailedOperation("File size is too big. please choose file have less 10Mb");
                if (EditingFile.ContentType.Contains("pdf"))
                {
                    FileBytes = await GetByteDataAsync(EditingFile);
                }
            },ActionType.UploadFile,false);
        }
        
        

        public async Task CreateDocumentFile()
        {
            await InvokeAsync(async () =>
            {
                if (NewFile is null)
                {
                    throw new FailedOperation("You've not import file yet");
                }

                var fileDto = await _uploadService.UploadDocumentFile(NewFile);
                NewDocumentFile.AbsolutePath = fileDto.Path;
                NewDocumentFile.FileName = fileDto.FileName;
                NewDocumentFile.Extentions = fileDto.Extension;
                NewDocumentFile.URL = fileDto.Url;
                NewDocumentFile.CreatedByUserName = await GetUserNameAsync();
                
                await _documentFileService.CreateAsync(input: NewDocumentFile);
                HideNewModal();
                await GetDocumentFiles();
            }, ActionType.Create, true);
        }

        public async Task UpdateDocumentFile()
        {
            await InvokeAsync(async () =>
            {
                if (EditingFile is not null)
                {
                    var fileDto = await _uploadService.UploadDocumentFile(EditingFile);
                    EditingDocumentFile.AbsolutePath = fileDto.Path;
                    EditingDocumentFile.FileName = fileDto.FileName;
                    EditingDocumentFile.Extentions = fileDto.Extension;
                    EditingDocumentFile.URL = fileDto.Url;

                }
     
                
                await _documentFileService.UpdateAsync(EditingDocumentFile, EditingDocumentFileId);
                HideEditingModal();
                await GetDocumentFiles();
            },ActionType.Update,true);
            
        }

        public async Task DeleteDocumentFile(Guid id)
        {
            await InvokeAsync(async () =>
            {
                await _documentFileService.DeleteAsync(id);
                HideEditingModal();
                await GetDocumentFiles();
            },ActionType.Delete,true);
            
        }

        public async Task ShowConfirmMessage(Guid id)
        {
            if (await _messageService.Confirm("Are you sure you want to confirm?", "Confirmation"))
            {
                await DeleteDocumentFile(id);
            }
        }

        public void ShowViewModal(DocumentFileDto dto)
        {
            ViewDocumentFile = dto;
            ViewModal.Show();
        }

        public void HideViewModal()
        {
            ViewModal.Hide();
        }
        
        
        public void ShowNewModal()
        {
            
            NewDocumentFile = new CreateUpdateDocumentFileDto();
            CreateModal.Show();
        }

        public void HideNewModal()
        {
            CreateModal.Hide();
        }


        public Task ShowEditingModal(DocumentFileDto dto)
        {
            EditingDocumentFile = new CreateUpdateDocumentFileDto();
            EditingDocumentFile = ObjectMapper.Map<DocumentFileDto, CreateUpdateDocumentFileDto>(dto);
            EditingDocumentFileId = dto.Id;
            EditingFile = null;
            return EditingModal.Show();
        }

        public void HideEditingModal()
        {
            EditingModal.Hide();
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
        void OnChangeSelectedFolder(TreeEventArgs args)
        {
            SelectedFolder = (FileFolderDto) args.Value;
            Filter.DocumentFolderId = SelectedFolder.Id;
        }

        async Task OnEnterKeyPressed(KeyboardEventArgs value)
        {
            
            if (value.Key == "Enter")
            {
                await GetDocumentFiles();
            }
        }

         async Task DownloadFile(string url,Guid documentFileId)
        {
            _navigationManager.NavigateTo(url);
            await _documentFileService.UpdateDownloadCountAsync(documentFileId);
            await GetDocumentFiles();
            StateHasChanged();
        }

         async Task PrintFile(Guid documentFileId)
         {
             await _documentFileService.UpdatePrintCountAsync(documentFileId);

         }
         
    }
}