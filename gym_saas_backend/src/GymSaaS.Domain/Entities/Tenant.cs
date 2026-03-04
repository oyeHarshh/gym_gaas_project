using GymSaaS.Domain.Common;
using GymSaaS.Domain.Enums;

namespace GymSaaS.Domain.Entities;

public class Tenant : BaseEntity
{
    public string GymName { get; private set; } = string.Empty;
    public string OwnerName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public SubscriptionTier Tier { get; private set; } = SubscriptionTier.Starter;
    public bool IsTrialActive { get; private set; } = true;
    public DateTime TrialEndsAt { get; private set; }
    public DateTime? SubscriptionEndsAt { get; private set; }
    public bool IsActive { get; private set; } = true;

    // Navigation
    public ICollection<User> Users { get; private set; } = new List<User>();
    public ICollection<Member> Members { get; private set; } = new List<Member>();
    public ICollection<Lead> Leads { get; private set; } = new List<Lead>();

    private Tenant() { }

    public static Tenant Create(string gymName, string ownerName, string email, string phone, string city)
    {
        return new Tenant
        {
            GymName = gymName,
            OwnerName = ownerName,
            Email = email,
            Phone = phone,
            City = city,
            TrialEndsAt = DateTime.UtcNow.AddDays(14)
        };
    }

    public void Activate(SubscriptionTier tier, int months)
    {
        Tier = tier;
        IsTrialActive = false;
        SubscriptionEndsAt = DateTime.UtcNow.AddMonths(months);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }
}
