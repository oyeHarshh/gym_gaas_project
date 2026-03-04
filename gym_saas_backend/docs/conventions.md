# Conventions & Coding Standards

> Update this file when: establishing new patterns or changing existing conventions.

---

## Naming

| Thing | Convention | Example |
|---|---|---|
| Classes | PascalCase | `MemberService` |
| Interfaces | I + PascalCase | `IDbHelperService` |
| Methods | PascalCase | `GetByIdAsync` |
| Variables | camelCase | `tenantId` |
| DTOs | PascalCase + suffix | `RegisterRequest`, `AuthResponse` |
| DB Tables | Plural PascalCase | `Members`, `MembershipPlans` |
| Files | Match class name | `MemberService.cs` |

---

## Folder Structure per Layer

### Domain
```
GymSaaS.Domain/
├── Entities/      ← one file per entity
├── Enums/         ← one file per enum
└── Common/        ← BaseEntity, TenantEntity
```

### Core
```
GymSaaS.Core/
├── DTOs/
│   └── <Module>/  ← e.g. Auth/, Members/
└── Interfaces/    ← IDbHelperService, IAuthService, etc.
```

### Services
```
GymSaaS.Services/
└── <Module>/      ← e.g. Auth/AuthService.cs, Members/MemberService.cs
```

### Infrastructure
```
GymSaaS.Infrastructure/
├── Migrations/
└── Persistence/
    ├── AppDbContext.cs
    ├── DbHelperService.cs
    └── Configurations/   ← one file per entity config
```

### API
```
GymSaaS.API/
├── Controllers/   ← one file per module
└── Program.cs
```

---

## Entity Rules
- All entities inherit `BaseEntity` or `TenantEntity`
- Private constructor + static `Create()` factory method
- Properties have `private set` — only mutated via methods
- No public setters on domain entities

```csharp
// Correct
public static Member Create(Guid tenantId, string name, ...) { ... }
public void AssignTrainer(Guid trainerId) { ... }

// Wrong
public string Name { get; set; }  // never public set
```

---

## Service Rules
- Inject only `IDbHelperService _db` and `IConfiguration` (if needed)
- Never inject `AppDbContext` directly into services
- `TenantId` always comes from JWT, never from request body
- Business logic lives in services, not controllers

---

## Controller Rules
- Only handle HTTP — no business logic
- Extract `tenantId` from JWT claims, pass to service
- Return appropriate HTTP status codes

```csharp
200 OK          → successful read/update
201 Created     → successful insert (with Location header if possible)
400 Bad Request → validation error
401 Unauthorized → auth failed
403 Forbidden   → valid token but insufficient role
404 Not Found   → entity not found
409 Conflict    → duplicate (email, etc.)
```

---

## DTOs
- Request DTOs: named `<Action>Request` — e.g. `CreateMemberRequest`
- Response DTOs: named `<Entity>Response` — e.g. `MemberResponse`
- Never return domain entities directly from controllers — always map to DTOs

---

## Database
- Soft delete only — never hard delete (`IsDeleted = true`)
- All money fields: `numeric(10,2)`
- All enums stored as `string` in DB (readable, migration-safe)
- Always index `TenantId` + commonly filtered columns

---

## ExFAT Gotcha
Before every build on this machine:
```bash
find . -name "._*" -delete
```
macOS creates ghost metadata files on ExFAT drives that break the C# compiler.
