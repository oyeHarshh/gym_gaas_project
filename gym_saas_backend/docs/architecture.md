# Architecture

> Update this file when: adding new layers, services, patterns, or changing project structure.

---

## Pattern: Clean Architecture + DDD

Dependencies flow inward — outer layers depend on inner, never the reverse:

```
GymSaaS.API  →  GymSaaS.Services  →  GymSaaS.Core  ←  GymSaaS.Domain
                      ↓
              GymSaaS.Infrastructure
```

| Layer | Project | Responsibility |
|---|---|---|
| Domain | `GymSaaS.Domain` | Entities, enums, base classes. No dependencies. |
| Core | `GymSaaS.Core` | DTOs, interfaces. Depends on Domain only. |
| Services | `GymSaaS.Services` | Business logic. Depends on Core + Domain + Infrastructure. |
| Infrastructure | `GymSaaS.Infrastructure` | EF Core, DbContext, migrations. Implements Core interfaces. |
| API | `GymSaaS.API` | Controllers, DI registration, middleware. |

---

## DbHelperService Pattern

A single generic service handles all database operations. **No individual repositories registered in DI.**

### Interface — `GymSaaS.Core/Interfaces/IDbHelperService.cs`
```csharp
// ONE injection covers ALL entities
private readonly IDbHelperService _db;

// Read
await _db.GetByIdAsync<Member>(id);
await _db.GetAllAsync<Member>(m => m.TenantId == tenantId);
await _db.FindAsync<User>(u => u.Email == email);
await _db.ExistsAsync<Tenant>(t => t.Email == email);
await _db.CountAsync<Member>(m => m.TenantId == tenantId);

// Write (overloaded — single or bulk)
await _db.InsertAsync(member);
await _db.InsertAsync(members);        // IEnumerable overload
await _db.UpdateAsync(member);
await _db.UpdateAsync(members);        // IEnumerable overload
await _db.DeleteAsync<Member>(id);     // soft delete
await _db.DeleteAsync<Member>(ids);    // bulk soft delete

// Complex queries
await _db.Query<Member>()
    .Include(m => m.Memberships)
    .Where(m => m.TenantId == tenantId)
    .OrderBy(m => m.Name)
    .ToListAsync();
```

### Implementation — `GymSaaS.Infrastructure/Persistence/DbHelperService.cs`
- Uses `AppDbContext` internally
- All reads automatically filter `IsDeleted == false`
- Delete is always soft delete (sets `IsDeleted = true`)
- Swap DB provider here only — zero changes to services

### DI Registration — `Program.cs`
```csharp
builder.Services.AddScoped<IDbHelperService, DbHelperService>();
```

---

## Multi-Tenancy

Every tenant entity inherits `TenantEntity` which adds `TenantId`.

- All queries in services must filter by `TenantId` extracted from JWT claims
- `TenantId` is embedded in the JWT token at login/register
- No cross-tenant data leakage by design

```csharp
// Extract in controller/service
var tenantId = Guid.Parse(User.FindFirst("tenantId")!.Value);

// Always filter by tenant
await _db.GetAllAsync<Member>(m => m.TenantId == tenantId);
```

---

## Services Registered in DI
| Interface | Implementation | Notes |
|---|---|---|
| `IDbHelperService` | `DbHelperService` | Generic — covers all entities |
| `IAuthService` | `AuthService` | Register + Login |

> Add new services here when registered.
