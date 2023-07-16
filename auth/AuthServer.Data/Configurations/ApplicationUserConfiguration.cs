using AuthServer.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AuthServer.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Ignore(u => u.ConcurrencyStamp)
                   .Ignore(u => u.PhoneNumber)
                   .Ignore(u => u.PhoneNumberConfirmed)
                   .Ignore(u => u.TwoFactorEnabled)
                   .Ignore(u => u.LockoutEnd)
                   .Ignore(u => u.AccessFailedCount);

            var USER_ID = new Guid("17c52fda-d109-44cf-a64c-9dbfc00f24b8");
            var user = new ApplicationUser
            {
                Id = USER_ID,
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                ConcurrencyStamp = USER_ID.ToString(),
                SecurityStamp = USER_ID.ToString(),
            };
            user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, "admin");
            builder.HasData(user);


        }
    }
}
