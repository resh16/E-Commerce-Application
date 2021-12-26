using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CustomIdentityProject.Model
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppRole, Guid, UserClaims, UserRole, UserLogin, RoleClaims,UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}


        protected override  void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("AppUser");

               
            });

            builder.Entity<AppRole>(b =>
            {
                
                b.ToTable("Roles");


            });

            builder.Entity<RoleClaims>(b =>
            {
               
                b.ToTable("RoleClaims");


            });

            builder.Entity<UserClaims>(b =>
            {
               
                b.ToTable("UserClaims");


            });

            builder.Entity<UserLogin>(b =>
            {
                
                b.ToTable("UserLogins");


            });

            builder.Entity<UserRole>(b =>
            {
               
                b.ToTable("UserRoles");


            });

            builder.Entity<UserToken>(b =>
            {
               
                b.ToTable("UserTokens");


            });


        }

    }
}
