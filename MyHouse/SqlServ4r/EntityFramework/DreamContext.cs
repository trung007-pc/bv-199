using System;
using Domain.Identity.RoleClaims;
using Domain.Identity.Roles;
using Domain.Identity.UserClaim;
using Domain.Identity.UserLogins;
using Domain.Identity.UserRoles;
using Domain.Identity.Users;
using Domain.Identity.UserTokens;
using Domain.Tests;
using Domain.UnitReviewDetails;
using Domain.UnitReviews;
using Domain.Units;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SqlServ4r.EntityFramework
{
    public class DreamContext  : IdentityDbContext<User,Role,Guid,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {
        public DbSet<UnitReview> UnitReviews { get; set;}
        public DbSet<Unit> Units { get; set;}
        public DbSet<UnitReviewDetail> UnitReviewDetails { get; set;}
        

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


            
            
            builder.Entity<UnitReviewDetail>().HasKey(sc => new { sc.Id });
            builder.Entity<UnitReviewDetail>().HasOne<UnitReview>(x => x.UnitReview)
                .WithMany(x => x.UnitReviewDetails)
                .HasForeignKey(x=>x.UnitReviewId);
            
            builder.Entity<UnitReviewDetail>().HasOne<Unit>(x => x.Unit)
                .WithMany(x => x.UnitReviewDetails)
                .HasForeignKey(x=>x.UnitId);


        }
        
        
        
        
        
    }
}