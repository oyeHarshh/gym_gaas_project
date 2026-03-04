# Database

> Update this file when: adding/removing entities, changing columns, adding indexes, or running new migrations.

---

## Connection
- **Host:** localhost:5432
- **Database:** gymsaas
- **App User:** gymsaas_user / gymsaas_pass
- **Superuser:** harshsaxena
- **Provider:** PostgreSQL 16 via Npgsql EF Core

---

## Approach
**Code First** — C# entities define the schema. EF Core generates migrations.

```
Entity class changed → dotnet ef migrations add <Name> → dotnet ef database update
```

---

## Base Classes

### `BaseEntity` — all entities inherit this
| Column | Type | Notes |
|---|---|---|
| `Id` | `uuid` | Primary key, auto-generated |
| `CreatedAt` | `timestamptz` | Set on creation |
| `UpdatedAt` | `timestamptz` | Auto-updated on SaveChanges |
| `IsDeleted` | `boolean` | Soft delete flag |

### `TenantEntity` — all tenant-scoped entities inherit this
Adds `TenantId uuid` (FK → Tenants) on top of BaseEntity.

---

## Tables

### Tenants
Represents a gym (the SaaS customer). Root of all multi-tenant data.
| Column | Type | Constraints |
|---|---|---|
| GymName | varchar(200) | NOT NULL |
| OwnerName | varchar(200) | NOT NULL |
| Email | varchar(256) | NOT NULL, UNIQUE |
| Phone | varchar(15) | NOT NULL |
| City | varchar(100) | NOT NULL |
| Tier | text | Starter / Growth / Enterprise |
| IsTrialActive | boolean | |
| TrialEndsAt | timestamptz | 14 days from registration |
| SubscriptionEndsAt | timestamptz | nullable |
| IsActive | boolean | |

### Users
Gym staff with role-based access.
| Column | Type | Constraints |
|---|---|---|
| TenantId | uuid | FK → Tenants |
| Name | varchar(200) | NOT NULL |
| Email | varchar(256) | NOT NULL, UNIQUE per tenant |
| Phone | varchar(15) | NOT NULL |
| PasswordHash | text | BCrypt |
| Role | text | Owner / Manager / Trainer / FrontDesk |
| IsActive | boolean | |
| LastLoginAt | timestamptz | nullable |

### Members
Gym members (customers of the gym).
| Column | Type | Constraints |
|---|---|---|
| TenantId | uuid | FK → Tenants |
| Name | varchar(200) | NOT NULL |
| Phone | varchar(15) | NOT NULL, indexed per tenant |
| Email | varchar(256) | nullable |
| DateOfBirth | timestamptz | |
| ProfilePhotoUrl | text | nullable |
| Address | text | nullable |
| EmergencyContact | text | nullable |
| AssignedTrainerId | uuid | nullable, FK → Users |
| JoinedAt | timestamptz | |

### MembershipPlans
Plan templates created by the gym (e.g. "Monthly - ₹999").
| Column | Type | Constraints |
|---|---|---|
| TenantId | uuid | FK → Tenants |
| Name | varchar(200) | NOT NULL |
| Description | varchar(500) | nullable |
| Price | numeric(10,2) | |
| DurationInDays | int | |
| IsActive | boolean | |

### Memberships
A plan assigned to a member.
| Column | Type | Constraints |
|---|---|---|
| TenantId | uuid | FK → Tenants |
| MemberId | uuid | FK → Members |
| MembershipPlanId | uuid | FK → MembershipPlans |
| StartDate | timestamptz | |
| EndDate | timestamptz | indexed (expiry queries) |
| Status | text | Active / Expired / Cancelled / Paused |
| AmountPaid | numeric(10,2) | |

### Payments
| Column | Type | Constraints |
|---|---|---|
| TenantId | uuid | FK → Tenants |
| MemberId | uuid | FK → Members |
| MembershipId | uuid | nullable |
| Amount | numeric(10,2) | |
| Method | text | UPI / Cash / Card / BankTransfer |
| Status | text | Pending / Paid / Overdue / Refunded |
| TransactionReference | varchar(200) | nullable |
| Notes | text | nullable |
| PaidAt | timestamptz | |

### Leads
Prospective gym members in the sales pipeline.
| Column | Type | Constraints |
|---|---|---|
| TenantId | uuid | FK → Tenants |
| Name | varchar(200) | NOT NULL |
| Phone | varchar(15) | NOT NULL |
| Email | varchar(256) | nullable |
| Source | varchar(100) | Walk-in / Instagram / Referral etc. |
| Interest | varchar(200) | Weight Loss / Muscle Gain etc. |
| Status | text | New / Contacted / Trial / Converted / Lost — indexed |
| AssignedToUserId | uuid | nullable |
| TrialDate | timestamptz | nullable |
| Notes | text | nullable |

### LeadActivities
| Column | Type |
|---|---|
| TenantId | uuid |
| LeadId | uuid FK → Leads |
| PerformedByUserId | uuid |
| Action | text |
| Notes | text nullable |

### AutomationRules
| Column | Type |
|---|---|
| TenantId | uuid |
| Name | varchar(200) |
| Trigger | text (enum as string) |
| DelayInHours | int nullable |
| IsActive | boolean |

### AutomationActions
| Column | Type |
|---|---|
| AutomationRuleId | uuid FK → AutomationRules |
| ActionType | int |
| MessageTemplateId | uuid nullable |
| TaskDescription | text nullable |

### AutomationExecutionLogs
| Column | Type |
|---|---|
| TenantId | uuid |
| AutomationRuleId | uuid FK → AutomationRules |
| TargetEntityId | uuid nullable |
| TargetEntityType | text (Member / Lead) |
| Success | boolean |
| ErrorMessage | text nullable |
| ExecutedAt | timestamptz |

### MessageTemplates
| Column | Type |
|---|---|
| TenantId | uuid |
| Name | varchar(200) |
| Channel | text (WhatsApp / SMS / Email) |
| Body | text (supports {Name}, {ExpiryDate} variables) |
| IsActive | boolean |

### MessageLogs
| Column | Type |
|---|---|
| TenantId | uuid |
| RecipientMemberId | uuid nullable |
| RecipientLeadId | uuid nullable |
| RecipientPhone | text |
| Channel | int |
| Body | text |
| Status | int (Pending / Delivered / Failed) |
| ProviderMessageId | text nullable |
| ErrorMessage | text nullable |
| SentAt | timestamptz |

### ReviewRequests
| Column | Type |
|---|---|
| TenantId | uuid |
| MemberId | uuid FK → Members |
| ReviewLink | text |
| IsClicked | boolean |
| ClickedAt | timestamptz nullable |
| SentAt | timestamptz |

### AuditLogs
| Column | Type |
|---|---|
| TenantId | uuid |
| UserId | uuid nullable |
| EntityName | varchar(100) indexed |
| EntityId | uuid nullable |
| Action | varchar(100) |
| OldValues | text (JSON) nullable |
| NewValues | text (JSON) nullable |
| IpAddress | varchar(50) nullable |
| OccurredAt | timestamptz indexed with TenantId |

---

## Migrations
| Migration | Date | Description |
|---|---|---|
| `InitialSchema` | 2026-03-04 | All 15 tables created |

> Add new migration entries here when created.
