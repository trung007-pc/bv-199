using System;
using Domain.Departments;
using Domain.Identity.RoleClaims;
using Domain.Identity.Roles;
using Domain.Identity.UnitTypes;
using Domain.Identity.UserClaim;
using Domain.Identity.UserLogins;
using Domain.Identity.UserRoles;
using Domain.Identity.Users;
using Domain.Identity.UserTokens;
using Domain.Positions;
using Domain.Tests;
using Domain.UnitReviewDetails;
using Domain.UnitReviews;
using Domain.Units;
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




        public DreamContext(DbContextOptions<DreamContext> options):base(options)
        {
            
            
        }
        
        protected override void OnModelCreating (ModelBuilder builder) {

            base.OnModelCreating (builder); 

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
            
   
            
                

            //relative 1-n : position  - user
            builder.Entity<User>().HasOne<Position>(x => x.Position)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.PositionId).IsRequired(false);
            
        
            
            // relative n-n : user - user_department -  department
            
            builder.Entity<UserDepartment>().HasKey(sc => new { sc.Id,sc.DepartmentId,sc.UserId });
            builder.Entity<UserDepartment>().HasOne<User>(x => x.User)
                .WithMany(x => x.UserDepartments)
                .HasForeignKey(x=>x.UserId);
            
            builder.Entity<UserDepartment>().HasOne<Department>(x => x.Department)
                .WithMany(x => x.UserDepartments)
                .HasForeignKey(x=>x.DepartmentId);

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
            
            
        }
        
        
        
        
        
    }
}