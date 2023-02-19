using Contract.FileTypes;
using Contract.IssuingAgencys;
using Core.Enum;
using Domain.DocumentFiles;
using Domain.IssuingAgencys;

namespace Contract.DocumentFiles
{
    public class DocumentFileWithNavPropertiesDto
    {
        public DocumentFileDto File { get; set; }
        
        public IssuingAgencyDto IssuingAgency { get; set; }
        
        public FileTypeDto FileType { get; set; }
    }
}