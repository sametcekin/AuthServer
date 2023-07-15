using AuthServer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AuthServer.Data.Configurations
{
    public class RoleAppConfiguration : IEntityTypeConfiguration<RoleApp>
    {
        public void Configure(EntityTypeBuilder<RoleApp> builder)
        {
            var ROLE_ID = new Guid("e7f3ee6f-ffb7-4654-a958-ec6c898c09cd");
            builder.HasData(new RoleApp { Id = ROLE_ID, Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = ROLE_ID.ToString() });
        }
    }
}
