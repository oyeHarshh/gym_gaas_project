using GymSaaS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSaaS.Infrastructure.Persistence.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Name).IsRequired().HasMaxLength(200);
        builder.Property(m => m.Phone).IsRequired().HasMaxLength(15);
        builder.Property(m => m.Email).HasMaxLength(256);
        builder.HasIndex(m => new { m.TenantId, m.Phone });

        builder.HasMany(m => m.Memberships).WithOne(ms => ms.Member).HasForeignKey(ms => ms.MemberId);
        builder.HasMany(m => m.Payments).WithOne(p => p.Member).HasForeignKey(p => p.MemberId);
    }
}
