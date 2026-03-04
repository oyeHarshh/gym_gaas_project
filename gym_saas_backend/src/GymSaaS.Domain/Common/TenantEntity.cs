namespace GymSaaS.Domain.Common;

public abstract class TenantEntity : BaseEntity
{
    public Guid TenantId { get; protected set; }
}
