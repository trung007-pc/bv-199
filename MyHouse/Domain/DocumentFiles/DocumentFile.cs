using System;
using System.Collections.Generic;
using Domain.FileFolders;
using Domain.FileTypes;
using Domain.FileVersions;
using Domain.Identity.Users;
using Domain.IssuingAgencys;
using Domain.Notifications;
using Domain.SendingFiles;

namespace Domain.DocumentFiles
{
    public class DocumentFile
    {
        public Guid Id { get; set; }


        public string Name { get; set;}
        public string Code { get; set; }
        
        public DateTime PublicationDate { get; set; } = DateTime.Now;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        
        
        public string StorageCode { get; set; }
        public bool AllowDownloadAndPrint { get; set; }
        public bool IsDeleted { get; set; }
        public int Views { get; set; }
        public int DownloadCount { get; set; }
        public int PrintCount { get; set; }

        
        //media 
        public string FileName { get; set; }
        public string Extentions { get; set; }
        public string AbsolutePath { get; set; }
        public string URL { get; set; }
        //foreign key
        
        public Guid? IssuingAgencyId { get; set; }
        public Guid? DocumentTypeId { get; set; }
        public Guid? DocumentFolderId { get; set; }
        public Guid CreatedBy { get; set; }
        
        //naviagation
        
        public FileFolder FileFolder { get; set;}
        public User User { get; set;}
        public FileType FileType { get; set;}
        public List<FileVersion> FileVersions { get; set;}
        public List<SendingFile> SendingFiles { get; set;}
        public List<Notification> Notifications { get; set; }

        public IssuingAgency IssuingAgency { get; set; }



    }
}