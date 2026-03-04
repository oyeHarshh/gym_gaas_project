using GymSaaS.Domain.Common;
using GymSaaS.Domain.Enums;

namespace GymSaaS.Domain.Entities;

public class MessageLog : TenantEntity
{
    public Guid? RecipientMemberId { get; private set; }
    public Guid? RecipientLeadId { get; private set; }
    public string RecipientPhone { get; private set; } = string.Empty;
    public MessageChannel Channel { get; private set; }
    public string Body { get; private set; } = string.Empty;
    public MessageStatus Status { get; private set; } = MessageStatus.Pending;
    public string? ProviderMessageId { get; private set; }
    public string? ErrorMessage { get; private set; }
    public DateTime SentAt { get; private set; } = DateTime.UtcNow;

    private MessageLog() { }

    public static MessageLog Create(Guid tenantId, string phone, MessageChannel channel, string body, Guid? memberId = null, Guid? leadId = null)
    {
        return new MessageLog
        {
            TenantId = tenantId,
            RecipientPhone = phone,
            Channel = channel,
            Body = body,
            RecipientMemberId = memberId,
            RecipientLeadId = leadId
        };
    }

    public void MarkDelivered(string? providerMessageId = null)
    {
        Status = MessageStatus.Delivered;
        ProviderMessageId = providerMessageId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkFailed(string error)
    {
        Status = MessageStatus.Failed;
        ErrorMessage = error;
        UpdatedAt = DateTime.UtcNow;
    }
}
