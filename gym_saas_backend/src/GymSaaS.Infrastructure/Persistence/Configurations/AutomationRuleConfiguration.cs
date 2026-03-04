using GymSaaS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSaaS.Infrastructure.Persistence.Configurations;

public class AutomationRuleConfiguration : IEntityTypeConfiguration<AutomationRule>
{
    public void Configure(EntityTypeBuilder<AutomationRule> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Trigger).HasConversion<string>();

        builder.HasMany(r => r.Actions).WithOne(a => a.AutomationRule).HasForeignKey(a => a.AutomationRuleId);
        builder.HasMany(r => r.ExecutionLogs).WithOne(l => l.AutomationRule).HasForeignKey(l => l.AutomationRuleId);
    }
}
