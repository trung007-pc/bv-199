using System;

namespace Contract.DocumentFiles
{
    public class CreateUpdateDocumentFileDto
    {
        public string Name { get; set;}
        public string? Code { get; set; }

        public DateTime PublicationDate { get; set; } = DateTime.Now;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string? Note { get; set; }

        public bool IsPrint { get; set; }
        public bool IsDeleted { get; set; }
        
        //media
        public string FileName { get; set; }
        public string Extentions { get; set; }
        public string AbsolutePath { get; set; }
        
        //foreign key
        public Guid? IssuingAgencyId { get; set; }
        public Guid? DocumentTypeId { get; set; }
        public Guid DocumentFolderId { get; set; }
        
        
        
        
        public Guid? CreatedBy { get; set; }
        
        public string? CreatedByUserName { get; set; }
    }
}