using GymSaaS.Domain.Common;

namespace GymSaaS.Domain.Entities;

public class LeadActivity : TenantEntity
{
    public Guid LeadId { get; private set; }
    public Guid PerformedByUserId { get; private set; }
    public string Action { get; private set; } = string.Empty; // "Called", "WhatsApp sent", "Trial scheduled"
    public string? Notes { get; private set; }

    // Navigation
    public Lead Lead { get; private set; } = null!;

    private LeadActivity() { }

    public static LeadActivity Create(Guid tenantId, Guid leadId, Guid userId, string action, string? notes = null)
    {
        return new LeadActivity
        {
            TenantId = tenantId,
            LeadId = leadId,
            PerformedByUserId = userId,
            Action = action,
            Notes = notes
        };
    }
}
