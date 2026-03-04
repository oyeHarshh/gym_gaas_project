using GymSaaS.Domain.Common;
using GymSaaS.Domain.Enums;

namespace GymSaaS.Domain.Entities;

public class Lead : TenantEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public string? Source { get; private set; }   // e.g. "Walk-in", "Instagram", "Referral"
    public string? Interest { get; private set; } // e.g. "Weight Loss", "Muscle Gain"
    public LeadStatus Status { get; private set; } = LeadStatus.New;
    public Guid? AssignedToUserId { get; private set; }
    public DateTime? TrialDate { get; private set; }
    public string? Notes { get; private set; }

    // Navigation
    public ICollection<LeadActivity> Activities { get; private set; } = new List<LeadActivity>();

    private Lead() { }

    public static Lead Create(Guid tenantId, string name, string phone, string? source = null, string? interest = null, string? email = null)
    {
        return new Lead
        {
            TenantId = tenantId,
            Name = name,
            Phone = phone,
            Email = email,
            Source = source,
            Interest = interest
        };
    }

    public void UpdateStatus(LeadStatus status)
    {
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignTo(Guid userId)
    {
        AssignedToUserId = userId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ScheduleTrial(DateTime trialDate)
    {
        TrialDate = trialDate;
        Status = LeadStatus.Trial;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Convert() => UpdateStatus(LeadStatus.Converted);
    public void MarkLost() => UpdateStatus(LeadStatus.Lost);
}
