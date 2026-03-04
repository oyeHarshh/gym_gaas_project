using GymSaaS.Domain.Common;

namespace GymSaaS.Domain.Entities;

public class ReviewRequest : TenantEntity
{
    public Guid MemberId { get; private set; }
    public string ReviewLink { get; private set; } = string.Empty;
    public bool IsClicked { get; private set; } = false;
    public DateTime? ClickedAt { get; private set; }
    public DateTime SentAt { get; private set; } = DateTime.UtcNow;

    // Navigation
    public Member Member { get; private set; } = null!;

    private ReviewRequest() { }

    public static ReviewRequest Create(Guid tenantId, Guid memberId, string reviewLink)
    {
        return new ReviewRequest
        {
            TenantId = tenantId,
            MemberId = memberId,
            ReviewLink = reviewLink
        };
    }

    public void RecordClick()
    {
        IsClicked = true;
        ClickedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
