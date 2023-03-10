using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contract.DocumentFiles;
using Contract.MyDashboards;
using Domain.DocumentFiles;
using Domain.FileFolders;
using Domain.FileTypes;
using Domain.IssuingAgencys;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using SqlServ4r.EntityFramework;
using SqlServ4r.RepGenerationPatten;
using Volo.Abp.DependencyInjection;

namespace SqlServ4r.Repository.FileDocuments
{
    public class DocumentFileRepository : GenericRepository<DocumentFile, Guid>, IDocumentFileRepository,
        ITransientDependency
    {
        public DocumentFileRepository([NotNull] DreamContext context) : base(context)
        {
        }

        public async Task<List<DocumentFileWithNavProperties>> GetFilesWithNavProperties(DocumentFileFilter filter)
        {

            var folderIds = new List<Guid?>();
            if (filter.DocumentFolderId.HasValue)
            {
                var result =await GetChildFolderIdAsync(filter.DocumentFolderId.Value);
                folderIds = result.Cast<Guid?>().ToList(); 
                folderIds.Add(filter.DocumentFolderId.Value);
               
            }
            
            var query = from file in _context.DocumentFiles.Where(x => 
                        !x.IsDeleted)
                    .WhereIf(filter.StartDay.HasValue && filter.EndDay.HasValue
                        , x => x.CreationDate >= filter.StartDay && x.CreationDate <= filter.EndDay)
                    .WhereIf(folderIds.Count > 0 , x=>folderIds.Contains(x.DocumentFolderId))
                    .WhereIf(!filter.Text.IsNullOrWhiteSpace(),
                        x => x.Name.Contains(filter.Text)|| x.Code.Contains(filter.Text))
                    .WhereIf(filter.FileTypeId.HasValue, x => x.DocumentTypeId == filter.FileTypeId)
                    .WhereIf(filter.IssuingAgencyId.HasValue, x => x.IssuingAgency.Id == filter.IssuingAgencyId)
                select new DocumentFileWithNavProperties
                {
                    File = file,
                    FileType = file.FileType ,
                    IssuingAgency = file.IssuingAgency,
                };


            return await query.ToListAsync();
        }

        public async Task<List<DocumentFileWithNavProperties>> GetUnreadDocumentFileOfUser(MyStatisticFilter filter)
        {
            var documentFiles =  await _context.SendingFiles
                .WhereIf(filter.EndDay is not null && filter.StartDay is not null ,
                    x=>x.SentDate >= filter.StartDay && x.SentDate <= filter.EndDay)    
                .Where(x => x.ReceiverId == filter.UserId && !x.Status)
                .Include(x => x.DocumentFile)
                .Select(x => new DocumentFileWithNavProperties()
                {
                    File = x.DocumentFile,
                    SendingFile = x
                }).ToListAsync();
           
            return documentFiles;
        }
        
        
        public async Task<List<DocumentFileWithNavProperties>> GetSharedFilesWithNavProperties(
            DocumentFileFilter filter
           )
        {
            var folderIds = new List<Guid?>();
            if (filter.DocumentFolderId.HasValue)
            {
                var result = await GetChildFolderIdAsync(filter.DocumentFolderId.Value);
                folderIds = result.Cast<Guid?>().ToList();
                folderIds.Add(filter.DocumentFolderId.Value);
            }
            
            var query = from file in _context.DocumentFiles.Where(x => 
                    !x.IsDeleted)   
                    .WhereIf(folderIds.Count > 0 , x=>folderIds.Contains(x.DocumentFolderId))
                    .WhereIf(!filter.Text.IsNullOrWhiteSpace(),
                        x => x.Name.Contains(filter.Text)|| x.Code.Contains(filter.Text))
                    .WhereIf(filter.FileTypeId.HasValue, x => x.DocumentTypeId == filter.FileTypeId)
                    .WhereIf(filter.IssuingAgencyId.HasValue, x => x.IssuingAgency.Id == filter.IssuingAgencyId)
                join sendingFile in _context.SendingFiles.Where(x=>x.ReceiverId == filter.UserId) 
                    on file.Id equals sendingFile.FileId 
                select new DocumentFileWithNavProperties
                {
                    File = file,
                    FileType = file.FileType ,
                    IssuingAgency = file.IssuingAgency,
                    SendingFile = sendingFile
                };
            
            return await query.ToListAsync();
        }

        
        private async Task<List<Guid>> GetChildFolderIdAsync(Guid parentID)
        {
            var folders = await _context.FileFolders.AsNoTracking().ToListAsync();
            var childIds = new List<Guid>();
            childIds = HandleRecursiveChildFolderId(parentID,childIds,folders);
            return childIds;
        }
        
        
        
        
        private  List<Guid> HandleRecursiveChildFolderId(Guid parentId,List<Guid> ChildFolderIds,List<FileFolder> folders)
        {
           var childIds = folders.Where(x => x.ParentCode == parentId)
               .Select(x=>x.Id);
           ChildFolderIds.AddRange(childIds);
           foreach (var item in childIds)
           {
               HandleRecursiveChildFolderId(item,ChildFolderIds,folders);
           }

           return ChildFolderIds;
        }
    }
}