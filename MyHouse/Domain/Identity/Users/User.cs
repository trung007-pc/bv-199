using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Enum;
using Domain.Departments;
using Domain.DocumentFiles;
using Domain.FileVersions;
using Domain.Notifications;
using Domain.Positions;
using Domain.SendingFiles;
using Domain.UserDepartments;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity.Users
{
    public class User : IdentityUser<Guid>
    {
        public string EmployeeCode { get; set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; } = Gender.Unknown;
        public DateTime DOB { get; set; } = DateTime.Now;
        
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set;}
        
        

        
        public Guid? PositionId { get; set; }
        
        
        
        
        //naviagation
        public List<Notification> Notifications { get; set; }
        public List<SendingFile> SenderSendingFiles { get; set;}
        
        public List<SendingFile> ReceiverSendingFiles { get; set;}

        public Position Position { get; set; }
        public List<UserDepartment> UserDepartments { get; set;}
        public List<FileVersion> EditedFileVersions { get; set; }
        public List<DocumentFile> CreatedFiles { get; set; }
        
        

    }
}