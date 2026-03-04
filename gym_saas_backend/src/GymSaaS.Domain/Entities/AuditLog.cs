using GymSaaS.Domain.Common;

namespace GymSaaS.Domain.Entities;

public class AuditLog : TenantEntity
{
    public Guid? UserId { get; private set; }
    public string EntityName { get; private set; } = string.Empty; // "Member", "Payment", etc.
    public Guid? EntityId { get; private set; }
    public string Action { get; private set; } = string.Empty;     // "Created", "Updated", "Deleted", "Login"
    public string? OldValues { get; private set; }  // JSON snapshot
    public string? NewValues { get; private set; }  // JSON snapshot
    public string? IpAddress { get; private set; }
    public DateTime OccurredAt { get; private set; } = DateTime.UtcNow;

    private AuditLog() { }

    public static AuditLog Create(Guid tenantId, Guid? userId, string entityName, Guid? entityId, string action, string? oldValues = null, string? newValues = null, string? ip = null)
    {
        return new AuditLog
        {
            TenantId = tenantId,
            UserId = userId,
            EntityName = entityName,
            EntityId = entityId,
            Action = action,
            OldValues = oldValues,
            NewValues = newValues,
            IpAddress = ip
        };
    }
}
