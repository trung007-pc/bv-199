using System;
using System.Collections.Generic;
using Domain.FileFolders;
using Domain.FileTypes;
using Domain.FileVersions;
using Domain.Identity.Users;
using Domain.IssuingAgencys;

namespace Domain.DocumentFiles
{
    public class DocumentFile
    {
        public Guid Id { get; set; }


        public string Name { get; set;}
        public string? Code { get; set; }
        
        public DateTime PublicationDate { get; set; } = DateTime.Now;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? Note { get; set; }
        
        
        public string StorageCode { get; set; }
        public bool IsPrint { get; set; }
        public bool IsDeleted { get; set; }
        public int Views { get; set; }
        public int DownloadCount { get; set; }
        
        
        //media 
        public string FileName { get; set; }
        public string Extentions { get; set; }
        public string AbsolutePath { get; set; }
        //foreign key
        
        public Guid? IssuingAgencyId { get; set; }
        public Guid? DocumentTypeId { get; set; }
        public Guid DocumentFolderId { get; set; }
        public Guid CreatedBy { get; set; }
        
        //naviagation
        
        public FileFolder FileFolder { get; set;}
        public User User { get; set;}
        public FileType FileType { get; set;}
        public List<FileVersion> FileVersions { get; set;}
        
        public IssuingAgency IssuingAgency { get; set; }



    }
}