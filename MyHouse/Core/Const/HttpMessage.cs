namespace Core.Const
{
    public static class HttpMessage
    {
        public const string NotFound = "Not Found";


        public const  string CheckInformation = "Check again information";
        public const  string Ok = "Successful";
        public static DuplicateMessage Duplicate = new DuplicateMessage();
        public const string ServerError = "Server Error";
        public const string InvalidExtension = "Invalid Extention";
        public const string Unauthorized = "Unauthorized";
        public const string Conflict = "The operation was updated by another user";
        public const  string EmptyContent = "Empty Content";


    }

    public  class DuplicateMessage
    {
        public  string DuplicateName = "Duplicate Name";
        public  string DuplicateEmail = "Duplicate Email";
        public  string DuplicatePhoneNumber = "Duplicate PhoneNumber";
        public string DuplicateEmployeeCode = "Duplicate Employee Code";
        public string DuplicateRoleCode = "Duplicate Code Role";
        public string DuplicateCode = "Duplicate Code";



    }
    
    
}