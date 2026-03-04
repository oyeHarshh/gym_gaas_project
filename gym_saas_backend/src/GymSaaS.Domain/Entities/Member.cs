using GymSaaS.Domain.Common;

namespace GymSaaS.Domain.Entities;

public class Member : TenantEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string? ProfilePhotoUrl { get; private set; }
    public string? Address { get; private set; }
    public string? EmergencyContact { get; private set; }
    public Guid? AssignedTrainerId { get; private set; }
    public DateTime JoinedAt { get; private set; } = DateTime.UtcNow;

    // Navigation
    public Tenant Tenant { get; private set; } = null!;
    public ICollection<Membership> Memberships { get; private set; } = new List<Membership>();
    public ICollection<Payment> Payments { get; private set; } = new List<Payment>();

    private Member() { }

    public static Member Create(Guid tenantId, string name, string phone, DateTime dateOfBirth, string? email = null)
    {
        return new Member
        {
            TenantId = tenantId,
            Name = name,
            Phone = phone,
            DateOfBirth = dateOfBirth,
            Email = email
        };
    }

    public void AssignTrainer(Guid trainerId)
    {
        AssignedTrainerId = trainerId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProfile(string? email, string? address, string? emergencyContact, string? photoUrl)
    {
        Email = email;
        Address = address;
        EmergencyContact = emergencyContact;
        ProfilePhotoUrl = photoUrl;
        UpdatedAt = DateTime.UtcNow;
    }
}
