using GymSaaS.Domain.Common;

namespace GymSaaS.Domain.Entities;

public class MembershipPlan : TenantEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public int DurationInDays { get; private set; }
    public bool IsActive { get; private set; } = true;

    // Navigation
    public Tenant Tenant { get; private set; } = null!;
    public ICollection<Membership> Memberships { get; private set; } = new List<Membership>();

    private MembershipPlan() { }

    public static MembershipPlan Create(Guid tenantId, string name, decimal price, int durationInDays, string? description = null)
    {
        return new MembershipPlan
        {
            TenantId = tenantId,
            Name = name,
            Price = price,
            DurationInDays = durationInDays,
            Description = description
        };
    }

    public void Update(string name, decimal price, int durationInDays, string? description)
    {
        Name = name;
        Price = price;
        DurationInDays = durationInDays;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
