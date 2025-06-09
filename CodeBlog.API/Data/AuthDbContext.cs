using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            
            var readerRoleId = "11147be6-18a8-4d71-9b62-f5305f365571";
            var writerRoleId = "22247be6-18a8-4d71-9b62-f5305f365571";

            // Create Reader and Writer Role
            var roles = new List<IdentityRole> 
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER",
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER",
                    ConcurrencyStamp = writerRoleId
                }

            };

            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Create Admin User
            var adminId = "66c42bea-e4c6-40aa-b366-b2ec2a3ec0c5";
            var admin = new IdentityUser()
            {
                Id = adminId,
                UserName = "admin@codeblog.com",
                Email = "admin@codeblog.com",
                NormalizedEmail = "admin@codeblog.com".ToUpper(),
                NormalizedUserName = "admin@codeblog.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);
            
            // Give the Admin User the Reader and Writer Role
            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>()
                {
                    UserId = adminId,
                    RoleId = readerRoleId
                },
                new IdentityUserRole<string>()
                {
                    UserId = adminId,
                    RoleId = writerRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }

    }
}
