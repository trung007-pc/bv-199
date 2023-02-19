using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contract.DocumentFiles;
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

            var query = from file in _context.Files.Where(x => !x.IsDeleted)
                select new DocumentFileWithNavProperties
                {
                    File = file,
                    FileType = file.FileType ,
                    IssuingAgency = file.IssuingAgency
                };

            var folderIds = new List<Guid>();
            if (filter.DocumentFolderId.HasValue)
            {
                folderIds = await GetChildFolderIdAsync(filter.DocumentFolderId.Value);
                folderIds.Add(filter.DocumentFolderId.Value);
            }
            
            
            query = query.WhereIf(filter.StartDay.HasValue && filter.EndDay.HasValue
                    , x => x.File.CreationDate >= filter.StartDay && x.File.CreationDate <= filter.EndDay)
                .WhereIf(!filter.Text.IsNullOrWhiteSpace(),
                    x=>x.File.Name.Contains(filter.Text)
                    || x.File.StorageCode.Contains(filter.Text)
                    )
                .WhereIf(filter.DocumentFolderId.HasValue, x => folderIds.Contains(x.File.DocumentFolderId))
                .WhereIf(filter.FileTypeId.HasValue, x => x.File.DocumentTypeId == filter.FileTypeId)
                .WhereIf(filter.IssuingAgencyId.HasValue, x => x.IssuingAgency.Id == filter.IssuingAgencyId);
                 
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