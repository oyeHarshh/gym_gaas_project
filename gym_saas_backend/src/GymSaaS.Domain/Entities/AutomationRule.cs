using GymSaaS.Domain.Common;
using GymSaaS.Domain.Enums;

namespace GymSaaS.Domain.Entities;

public class AutomationRule : TenantEntity
{
    public string Name { get; private set; } = string.Empty;
    public AutomationTrigger Trigger { get; private set; }
    public int? DelayInHours { get; private set; } // delay after trigger fires
    public bool IsActive { get; private set; } = true;

    // Navigation
    public ICollection<AutomationAction> Actions { get; private set; } = new List<AutomationAction>();
    public ICollection<AutomationExecutionLog> ExecutionLogs { get; private set; } = new List<AutomationExecutionLog>();

    private AutomationRule() { }

    public static AutomationRule Create(Guid tenantId, string name, AutomationTrigger trigger, int? delayInHours = null)
    {
        return new AutomationRule
        {
            TenantId = tenantId,
            Name = name,
            Trigger = trigger,
            DelayInHours = delayInHours
        };
    }

    public void Toggle()
    {
        IsActive = !IsActive;
        UpdatedAt = DateTime.UtcNow;
    }
}
