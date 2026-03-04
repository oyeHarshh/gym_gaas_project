using GymSaaS.Domain.Common;
using GymSaaS.Domain.Enums;

namespace GymSaaS.Domain.Entities;

public class MessageTemplate : TenantEntity
{
    public string Name { get; private set; } = string.Empty;
    public MessageChannel Channel { get; private set; }
    public string Body { get; private set; } = string.Empty; // supports {Name}, {ExpiryDate}, etc.
    public bool IsActive { get; private set; } = true;

    // Navigation
    public Tenant Tenant { get; private set; } = null!;

    private MessageTemplate() { }

    public static MessageTemplate Create(Guid tenantId, string name, MessageChannel channel, string body)
    {
        return new MessageTemplate
        {
            TenantId = tenantId,
            Name = name,
            Channel = channel,
            Body = body
        };
    }

    public void Update(string name, string body)
    {
        Name = name;
        Body = body;
        UpdatedAt = DateTime.UtcNow;
    }
}
