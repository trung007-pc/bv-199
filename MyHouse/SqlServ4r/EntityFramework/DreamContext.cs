using System;
using System.IO;
using Domain.Departments;
using Domain.DocumentFiles;
using Domain.FileFolders;
using Domain.FileTypes;
using Domain.FileVersions;
using Domain.Identity.RoleClaims;
using Domain.Identity.Roles;
using Domain.Identity.UserClaim;
using Domain.Identity.UserLogins;
using Domain.Identity.UserRoles;
using Domain.Identity.Users;
using Domain.Identity.UserTokens;
using Domain.IssuingAgencys;
using Domain.Notifications;
using Domain.Positions;
using Domain.SendingFiles;
using Domain.Tests;
using Domain.UnitReviewDetails;
using Domain.UnitReviews;
using Domain.Units;
using Domain.UnitTypes;
using Domain.UserDepartments;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SqlServ4r.EntityFramework
{
    public class DreamContext  : IdentityDbContext<User,Role,Guid,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {
        public DbSet<UnitReview> UnitReviews { get; set;}
        public DbSet<Unit> Units { get; set;}
        public DbSet<UnitReviewDetail> UnitReviewDetails { get; set;}
        public DbSet<UnitType> UnitTypes { get; set;}
        
        public DbSet<Position> Positions { get; set;}

        public DbSet<Department> Departments { get; set;}
        
        public DbSet<UserDepartment> UserDepartments { get; set;}
        
        public DbSet<FileFolder> FileFolders { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<IssuingAgency> IssuingAgencies { get; set; }
        public DbSet<DocumentFile> Files { get; set; }
        public DbSet<FileVersion>  FileVersions { get; set; }
        
        public DbSet<SendingFile> SendingFiles { get; set; }
        public DbSet<Notification> Notifications { get; set; }





        public DreamContext(DbContextOptions<DreamContext> options):base(options)
        {
            
            
        }
        
        protected override void OnModelCreating (ModelBuilder builder) {

            base.OnModelCreating (builder); 

            //default ondelete cascade
            //get rid of prefix Asp
            foreach (var entityType in builder.Model.GetEntityTypes ()) {
                var tableName = entityType.GetTableName ();
                if (tableName.StartsWith ("AspNet")) {
                    entityType.SetTableName (tableName.Substring (6));
                }
            }


            //relative n-n : unit - unit_reviewdetail - review
            builder.Entity<UnitReviewDetail>().HasKey(sc => new { sc.Id });
            builder.Entity<UnitReviewDetail>().HasOne<UnitReview>(x => x.UnitReview)
                .WithMany(x => x.UnitReviewDetails)
                .HasForeignKey(x=>x.UnitReviewId);
            
            builder.Entity<UnitReviewDetail>().HasOne<Unit>(x => x.Unit)
                .WithMany(x => x.UnitReviewDetails)
                .HasForeignKey(x=>x.UnitId);

            
            //relative 1-n : unit type - unit
            builder.Entity<Unit>().HasOne<UnitType>(x => x.UnitType)
                .WithMany(x => x.Units)
                .HasForeignKey(x => x.UnitTypeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            
            //================SS2
            
            //relative 1-n : position  - user
            builder.Entity<User>().HasOne<Position>(x => x.Position)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.PositionId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            
            
            // relative n-n : user - user_department -  department
            // when you delete department then it delete userdepartment record correspond 
            builder.Entity<UserDepartment>().HasKey(sc => new { sc.Id,sc.DepartmentId,sc.UserId });
            builder.Entity<UserDepartment>().HasOne<User>(x => x.User)
                .WithMany(x => x.UserDepartments)
                .HasForeignKey(x=>x.UserId);
            
            builder.Entity<UserDepartment>().HasOne<Department>(x => x.Department)
                .WithMany(x => x.UserDepartments)
                .HasForeignKey(x=>x.DepartmentId);

            
            
            
            
            //1-n
            builder.Entity<DocumentFile>().HasOne<IssuingAgency>(x => x.IssuingAgency)
                .WithMany(x => x.DocumentFiles)
                .HasForeignKey(x => x.IssuingAgencyId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            
            
            // if have a foreign key references to FileFolder then forbidden Deletetion
            builder.Entity<DocumentFile>().HasOne<FileFolder>(x => x.FileFolder)
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.DocumentFolderId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<DocumentFile>().HasOne<User>(x => x.User)
                .WithMany(x => x.CreatedFiles)
                .HasForeignKey(x => x.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DocumentFile>().HasOne<FileType>(x => x.FileType)
                .WithMany(x => x.DocumentFiles)
                .HasForeignKey(x => x.DocumentTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<FileVersion>().
                HasOne<DocumentFile>(x => x.DocumentFile)
                .WithMany(x => x.FileVersions)
                .HasForeignKey(x => x.FileId)
                .OnDelete(DeleteBehavior.Restrict);
            


            builder.Entity<FileVersion>().HasOne<User>(x => x.User)
                .WithMany(x => x.EditedFileVersions)
                .HasForeignKey(x => x.EditBy)
                .OnDelete(DeleteBehavior.Restrict);
            
            
            
            // user - sendingfile - file
            
            builder.Entity<SendingFile>().HasKey(sc => new { sc.Id,sc.FileId,sc.ReceiverId });
            
            builder.Entity<SendingFile>().HasOne<User>(x => x.User)
                .WithMany(x => x.SendingFiles)
                .HasForeignKey(x=>x.ReceiverId);
            
            builder.Entity<SendingFile>().HasOne<DocumentFile>(x => x.DocumentFile)
                .WithMany(x => x.SendingFiles)
                .HasForeignKey(x=>x.FileId);


            builder.Entity<Notification>().HasKey(sc => new { sc.Id
                ,sc.DestinationCode
                ,sc.ReceiverId });
            
            builder.Entity<Notification>().HasOne<User>(x => x.User)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x=>x.ReceiverId);
            
            builder.Entity<Notification>().HasOne<DocumentFile>(x => x.DocumentFile)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x=>x.DestinationCode);
            
            
            
            
            
            
            
            
            

            SetUniqueForProperties(builder);

        }

        public void SetUniqueForProperties(ModelBuilder builder)
        {
            builder.Entity<Role>(entity => {

                entity.HasIndex(p => p.Name)     
                    .IsUnique(true);
                entity.HasIndex(p => p.Code)     
                    .IsUnique(true);
            });
            
            builder.Entity<User>(entity => {
                entity.HasIndex(p => p.PhoneNumber)     
                    .IsUnique(true);
                entity.HasIndex(p => p.EmployeeCode)     
                    .IsUnique(true);
                entity.HasIndex(p => p.Email)     
                    .IsUnique(true);
            });
            
            builder.Entity<Unit>(entity => {

                entity.HasIndex(p => p.Name)     
                    .IsUnique(true);
            });
            
            builder.Entity<UnitType>(entity => {
                entity.HasIndex(p => p.Name)     
                    .IsUnique(true);
            });
            builder.Entity<UnitReview>(entity =>
            {
                entity.HasIndex(p => p.CreationDate);
            });
            
            builder.Entity<Position>(entity => {
                entity.HasIndex(p => p.Name)     
                    .IsUnique(true);
                entity.HasIndex(p => p.Code)     
                    .IsUnique(true);
            });
            
            builder.Entity<Department>(entity => {
                entity.HasIndex(p => p.Name)     
                    .IsUnique(true);
                entity.HasIndex(p => p.Code)     
                    .IsUnique(true);
            });
            
            builder.Entity<DocumentFile>(entity =>
            {

                entity.HasIndex(p => p.StorageCode)
                    .IsUnique(true);
                entity.HasIndex(p => p.Name)
                    .IsUnique(true);
                entity.HasIndex(p => p.Code)     
                    .IsUnique(true);
            });

            builder.Entity<FileType>(entity => {
                entity.HasIndex(p => p.Name)     
                    .IsUnique(true);
                entity.HasIndex(p => p.Code)     
                    .IsUnique(true);
            });
            builder.Entity<IssuingAgency>(entity => {
                entity.HasIndex(p => p.Name)     
                    .IsUnique(true);
                entity.HasIndex(p => p.Code)     
                    .IsUnique(true);
            });

            builder.Entity<FileFolder>(entity => {
                entity.HasIndex(p => p.Name)     
                    .IsUnique(true);
                entity.HasIndex(p => p.Code)     
                    .IsUnique(true);
            });
            
            
            
            
            
            
        }
        
        
        
        
        
    }
}