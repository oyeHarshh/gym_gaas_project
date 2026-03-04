using GymSaaS.Domain.Common;
using GymSaaS.Domain.Enums;

namespace GymSaaS.Domain.Entities;

public class Membership : TenantEntity
{
    public Guid MemberId { get; private set; }
    public Guid MembershipPlanId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public MembershipStatus Status { get; private set; } = MembershipStatus.Active;
    public decimal AmountPaid { get; private set; }

    // Navigation
    public Member Member { get; private set; } = null!;
    public MembershipPlan MembershipPlan { get; private set; } = null!;

    private Membership() { }

    public static Membership Create(Guid tenantId, Guid memberId, Guid planId, DateTime startDate, int durationDays, decimal amountPaid)
    {
        return new Membership
        {
            TenantId = tenantId,
            MemberId = memberId,
            MembershipPlanId = planId,
            StartDate = startDate,
            EndDate = startDate.AddDays(durationDays),
            AmountPaid = amountPaid
        };
    }

    public void Cancel()
    {
        Status = MembershipStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Expire()
    {
        Status = MembershipStatus.Expired;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsExpiringSoon(int withinDays = 7) =>
        Status == MembershipStatus.Active && EndDate <= DateTime.UtcNow.AddDays(withinDays);

    public bool IsExpired() =>
        EndDate < DateTime.UtcNow;
}
