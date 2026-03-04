using GymSaaS.Domain.Common;
using GymSaaS.Domain.Enums;

namespace GymSaaS.Domain.Entities;

public class Payment : TenantEntity
{
    public Guid MemberId { get; private set; }
    public Guid? MembershipId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentMethod Method { get; private set; }
    public PaymentStatus Status { get; private set; } = PaymentStatus.Pending;
    public string? TransactionReference { get; private set; }
    public string? Notes { get; private set; }
    public DateTime PaidAt { get; private set; }

    // Navigation
    public Member Member { get; private set; } = null!;

    private Payment() { }

    public static Payment Create(Guid tenantId, Guid memberId, decimal amount, PaymentMethod method, Guid? membershipId = null, string? notes = null)
    {
        return new Payment
        {
            TenantId = tenantId,
            MemberId = memberId,
            MembershipId = membershipId,
            Amount = amount,
            Method = method,
            Notes = notes,
            PaidAt = DateTime.UtcNow,
            Status = PaymentStatus.Paid
        };
    }

    public void MarkOverdue()
    {
        Status = PaymentStatus.Overdue;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetReference(string reference)
    {
        TransactionReference = reference;
        UpdatedAt = DateTime.UtcNow;
    }
}
