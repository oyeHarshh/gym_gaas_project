# Authentication & Authorization

> Update this file when: changing JWT structure, adding roles, or modifying auth flow.

---

## Flow

```
Register → Creates Tenant + Owner User → Returns JWT
Login    → Validates credentials        → Returns JWT
All other endpoints → Require JWT in Authorization header
```

---

## JWT Token

**Algorithm:** HMAC-SHA256
**Expiry:** 24 hours
**Key:** stored in `appsettings.json` under `Jwt:Key`

### Claims
| Claim | Value | Usage |
|---|---|---|
| `sub` | User ID (Guid) | Identify the user |
| `email` | User email | Display / lookup |
| `tenantId` | Tenant ID (Guid) | Row-level tenant isolation |
| `role` | Owner / Manager / Trainer / FrontDesk | RBAC |
| `gymName` | Gym name | Display in UI |

### Extract in controller
```csharp
var tenantId = Guid.Parse(User.FindFirst("tenantId")!.Value);
var userId   = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
var role     = User.FindFirst(ClaimTypes.Role)!.Value;
```

---

## Roles (RBAC)
| Role | Description |
|---|---|
| `Owner` | Full access — created on gym registration |
| `Manager` | Manage members, leads, payments |
| `Trainer` | View assigned members, attendance |
| `FrontDesk` | Check-in, add members, record payments |

### Protecting endpoints
```csharp
[Authorize]                           // any authenticated user
[Authorize(Roles = "Owner")]          // owner only
[Authorize(Roles = "Owner,Manager")]  // owner or manager
```

---

## Passwords
- Hashed with **BCrypt** (work factor 11)
- Plain text never stored or logged

---

## Tenant Isolation
- Every DB query in services must filter by `TenantId`
- `TenantId` comes from JWT — never from request body
- This ensures gym A can never see gym B's data
