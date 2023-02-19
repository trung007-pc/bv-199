using Domain.FileTypes;
using Domain.IssuingAgencys;

namespace Domain.DocumentFiles
{
    public class DocumentFileWithNavProperties
    {
        public DocumentFile File { get; set; }
        
        public IssuingAgency IssuingAgency { get; set; }
        
        public FileType FileType { get; set; }
    }
}