using GymSaaS.Domain.Common;

namespace GymSaaS.Domain.Entities;

public class AutomationExecutionLog : TenantEntity
{
    public Guid AutomationRuleId { get; private set; }
    public Guid? TargetEntityId { get; private set; } // MemberId or LeadId
    public string TargetEntityType { get; private set; } = string.Empty; // "Member" | "Lead"
    public bool Success { get; private set; }
    public string? ErrorMessage { get; private set; }
    public DateTime ExecutedAt { get; private set; } = DateTime.UtcNow;

    // Navigation
    public AutomationRule AutomationRule { get; private set; } = null!;

    private AutomationExecutionLog() { }

    public static AutomationExecutionLog Create(Guid tenantId, Guid ruleId, Guid? targetId, string targetType, bool success, string? error = null)
    {
        return new AutomationExecutionLog
        {
            TenantId = tenantId,
            AutomationRuleId = ruleId,
            TargetEntityId = targetId,
            TargetEntityType = targetType,
            Success = success,
            ErrorMessage = error
        };
    }
}
