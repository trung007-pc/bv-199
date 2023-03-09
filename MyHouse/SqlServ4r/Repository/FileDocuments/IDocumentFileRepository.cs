using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contract.DocumentFiles;
using Contract.MyDashboards;
using Domain.DocumentFiles;

namespace SqlServ4r.Repository.FileDocuments
{
    public interface IDocumentFileRepository
    {
        Task<List<DocumentFileWithNavProperties>> GetFilesWithNavProperties(DocumentFileFilter filter);
        
        Task<List<DocumentFileWithNavProperties>> GetSharedFilesWithNavProperties(DocumentFileFilter filter);

        Task<List<DocumentFileWithNavProperties>> GetUnreadDocumentFileOfUser(MyStatisticFilter filter);
    }
}