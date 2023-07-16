using AuthServer.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuthServer.Data.Configurations
{
    public class RoleClaimAppConfiguration : IEntityTypeConfiguration<RoleClaimApp>
    {
        public void Configure(EntityTypeBuilder<RoleClaimApp> builder)
        {
        }
    }
}
