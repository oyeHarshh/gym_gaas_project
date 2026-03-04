using GymSaaS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSaaS.Infrastructure.Persistence.Configurations;

public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.AmountPaid).HasColumnType("numeric(10,2)");
        builder.Property(m => m.Status).HasConversion<string>();
        builder.HasIndex(m => new { m.TenantId, m.MemberId });
        builder.HasIndex(m => m.EndDate); // for expiry queries
    }
}
