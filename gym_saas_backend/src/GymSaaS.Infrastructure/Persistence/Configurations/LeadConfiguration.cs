using GymSaaS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSaaS.Infrastructure.Persistence.Configurations;

public class LeadConfiguration : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Name).IsRequired().HasMaxLength(200);
        builder.Property(l => l.Phone).IsRequired().HasMaxLength(15);
        builder.Property(l => l.Email).HasMaxLength(256);
        builder.Property(l => l.Source).HasMaxLength(100);
        builder.Property(l => l.Interest).HasMaxLength(200);
        builder.Property(l => l.Status).HasConversion<string>();
        builder.HasIndex(l => new { l.TenantId, l.Status });

        builder.HasMany(l => l.Activities).WithOne(a => a.Lead).HasForeignKey(a => a.LeadId);
    }
}
