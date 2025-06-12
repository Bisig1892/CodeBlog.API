using CodeBlog.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeBlog.API.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "28d65a5b-a7db-4850-b380-83591f7d7531";
            var writerRoleId = "9740f16c-24a1-4224-a7be-1bb00b7c6892";
            var adminUserId = "edc267ec-d43c-4e3b-8108-a1a1f819906d";

            // Roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER"
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER"
                }
            );

            // User
            var admin = new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin@codeblog.com",
                Email = "admin@codeblog.com",
                NormalizedEmail = "ADMIN@CODEBLOG.COM",
                NormalizedUserName = "ADMIN@CODEBLOG.COM",
                PasswordHash = "AQAAAAIAAYagAAAAEHn557F4KVaSgKo2hDGL9ZBdpUjgt1bkFMEC+/s5V7twbJyz2hhaao/Jk1KYnVvnMQ==",
                SecurityStamp = "c1a16e7a - 5f4b - 4d7b - 8eaa - 123456789abc",
                ConcurrencyStamp = "b1e24f2a-cf3b-49a7-9b29-6b1c4e2a5642"
            };

            builder.Entity<IdentityUser>().HasData(admin);

            // User Roles
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = readerRoleId,
                    UserId = adminUserId
                },
                new IdentityUserRole<string>
                {
                    RoleId = writerRoleId,
                    UserId = adminUserId
                }
            );
        }

    }
}