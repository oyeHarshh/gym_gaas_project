using GymSaaS.Domain.Common;
using GymSaaS.Domain.Enums;

namespace GymSaaS.Domain.Entities;

public class AutomationAction : BaseEntity
{
    public Guid AutomationRuleId { get; private set; }
    public AutomationActionType ActionType { get; private set; }
    public Guid? MessageTemplateId { get; private set; } // for WhatsApp/SMS/Email actions
    public string? TaskDescription { get; private set; } // for AssignTask action

    // Navigation
    public AutomationRule AutomationRule { get; private set; } = null!;

    private AutomationAction() { }

    public static AutomationAction Create(Guid ruleId, AutomationActionType actionType, Guid? templateId = null, string? taskDescription = null)
    {
        return new AutomationAction
        {
            AutomationRuleId = ruleId,
            ActionType = actionType,
            MessageTemplateId = templateId,
            TaskDescription = taskDescription
        };
    }
}
