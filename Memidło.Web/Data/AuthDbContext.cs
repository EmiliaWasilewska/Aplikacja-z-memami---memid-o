using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Memidło.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var superAdminRoleId = "1ddcea3f-950f-4958-9a10-982c2a2cea1a";
            var adminRoleId = "d035d02e-43d7-44d7-9ab4-5b11debf4f08";
            var userRoleId = "14f2c7a9-30c6-48b1-92d4-f4b835680d91";

            //Seed Roles (User, Admin, Super Admin)
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                 new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                  new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Seed Super Admin
            var superAdminId = "238d88bb-1ef5-47f3-bc34-0963518df061";
            var superAdminUser = new IdentityUser()
            {
                Id = superAdminId,
                UserName = "superadmin@somemail.com",
                Email = "superadmin@somemail.com",
                NormalizedEmail = "superadmin@somemail.com".ToUpper(),
                NormalizedUserName = "superadmin@somemail.com".ToUpper()
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "IneedTh1sJo&");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add all roles to Super Admin
            var allRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string> 
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                 new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(allRoles);
        }
    }
}
