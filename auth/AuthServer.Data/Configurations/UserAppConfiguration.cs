using AuthServer.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AuthServer.Data.Configurations
{
    public class UserAppConfiguration : IEntityTypeConfiguration<UserApp>
    {
        public void Configure(EntityTypeBuilder<UserApp> builder)
        {
            var USER_ID = new Guid("17c52fda-d109-44cf-a64c-9dbfc00f24b8");
            var user = new UserApp
            {
                Id = USER_ID,
                Email = "admin@admin.com",
                EmailConfirmed = true,
                UserName = "admin",
                NormalizedEmail = "ADMIN"
            };
            user.PasswordHash = new PasswordHasher<UserApp>().HashPassword(user, "admin");
            builder.HasData(user);
        }
    }
}
