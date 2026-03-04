using GymSaaS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSaaS.Infrastructure.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.GymName).IsRequired().HasMaxLength(200);
        builder.Property(t => t.OwnerName).IsRequired().HasMaxLength(200);
        builder.Property(t => t.Email).IsRequired().HasMaxLength(256);
        builder.Property(t => t.Phone).IsRequired().HasMaxLength(15);
        builder.Property(t => t.City).IsRequired().HasMaxLength(100);
        builder.Property(t => t.Tier).HasConversion<string>();
        builder.HasIndex(t => t.Email).IsUnique();

        builder.HasMany(t => t.Users).WithOne(u => u.Tenant).HasForeignKey(u => u.TenantId);
        builder.HasMany(t => t.Members).WithOne(m => m.Tenant).HasForeignKey(m => m.TenantId);
        builder.HasMany(t => t.Leads).WithOne().HasForeignKey(l => l.TenantId);
    }
}
