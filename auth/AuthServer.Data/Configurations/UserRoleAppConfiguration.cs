using AuthServer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AuthServer.Data.Configurations
{
    public class UserRoleAppConfiguration : IEntityTypeConfiguration<UserRoleApp>
    {
        public void Configure(EntityTypeBuilder<UserRoleApp> builder)
        {
            var userRole = new UserRoleApp { UserId = new Guid("17c52fda-d109-44cf-a64c-9dbfc00f24b8"), RoleId = new Guid("e7f3ee6f-ffb7-4654-a958-ec6c898c09cd") };
            builder.HasData(userRole);
        }
    }
}
