using AuthServer.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Data.Configurations
{
    public class RoleClaimAppConfiguration : IEntityTypeConfiguration<RoleClaimApp>
    {
        public void Configure(EntityTypeBuilder<RoleClaimApp> builder)
        {
        }
    }
}
