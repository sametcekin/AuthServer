using AuthServer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AuthServer.Data.Configurations
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            var ROLE_ID = new Guid("e7f3ee6f-ffb7-4654-a958-ec6c898c09cd");
            builder.HasData(new ApplicationRole { Id = ROLE_ID, Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = ROLE_ID.ToString() });
        }
    }
}
