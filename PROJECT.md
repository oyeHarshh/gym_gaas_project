# Gym SaaS — Redwood Labs
### Membership Renewal & Lead Conversion Automation for Gyms

---

## Project Overview

**Product Name:** Redwood Labs
**Product Type:** Multi-tenant SaaS platform
**Target Users:** Gym owners (non-technical), managers, trainers, front desk staff
**Primary Goal:** Automate membership renewals, lead conversion, and member communications so gym owners stop losing members to forgetfulness
**Market:** India-first (₹ pricing, WhatsApp-first, UPI payments)

---

## Current Status

| Phase | Status |
|---|---|
| Technical Documentation | ✅ Complete (`Gym_SaaS_Technical_Documentation.docx`) |
| Design Prototype (HTML) | ✅ Complete (`design-prototype.html`) |
| Backend Implementation | 🔜 Not started |
| Frontend Implementation | 🔜 Not started |

---

## Tech Stack (Decided)

| Layer | Technology |
|---|---|
| **Backend** | .NET 8 — ASP.NET Core Web API |
| **Architecture** | Clean Architecture + Domain-Driven Design (DDD) |
| **Database** | PostgreSQL (with JSONB for dynamic config) |
| **Caching** | Redis |
| **Background Jobs** | Hangfire (Phase 1) → RabbitMQ/Kafka (Phase 2) |
| **Frontend** | React (TypeScript) |
| **Messaging** | WhatsApp Cloud API · SMS Provider Abstraction · Email (SendGrid / AWS SES) |
| **Auth** | JWT + RBAC + Row-level tenant isolation |
| **DevOps** | Docker · GitHub Actions CI/CD · Azure / AWS |

---

## Branding

| Property | Value |
|---|---|
| **Brand Name** | Redwood Labs |
| **Primary Color** | `#C1440E` (Redwood red-orange) |
| **Primary Dark** | `#9A3309` |
| **Primary Light** | `#FDF0EB` |
| **Sidebar** | `#14202E` (Dark navy) |
| **Background** | `#F3F1EE` (Warm off-white) |
| **Font** | Inter (Google Fonts) |
| **Icon** | `fa-seedling` (Font Awesome) |
| **Design Style** | Modern SaaS · Card-based · Non-tech friendly · India-focused |

---

## Architecture — Module Map

### 1. Multi-Tenant & Subscription Management
- Gym registration and onboarding
- Subscription plan assignment (Starter / Growth / Enterprise)
- Trial and active subscription lifecycle
- Feature gating by subscription plan

**DB Tables:** `Tenants`, `SubscriptionPlans`, `TenantSettings`

---

### 2. User & Role Management (RBAC)
- Roles: **Owner**, **Manager**, **Trainer**, **Front Desk**
- Permission matrix controls what each role can access
- Row-level tenant isolation — each gym only sees its own data

**DB Tables:** `Users`, `Roles`, `Permissions`, `RolePermissions`

---

### 3. Lead Management Engine
- Manual lead entry + CSV import
- Status pipeline: `New → Contacted → Trial → Converted → Lost`
- Lead activity tracking and assignment
- Visual kanban board (in prototype)

**DB Tables:** `Leads`, `LeadActivities`

---

### 4. Automation Engine (Core System)
- **Triggers:** Lead Created · Trial Scheduled · Membership Expiry · No Payment After X Days
- **Actions:** Send WhatsApp · Send SMS · Send Email · Assign Task
- Rules are configurable per gym (no code needed)

**DB Tables:** `AutomationRules`, `AutomationActions`, `AutomationExecutionLogs`

---

### 5. Membership Lifecycle Management
- Create and manage membership plans (duration, price, conditions)
- Assign memberships to members
- Expiry tracking with automated reminders at 7 / 3 / 1 days

**DB Tables:** `MembershipPlans`, `Members`, `Memberships`

---

### 6. Payment Tracking
- Record payments (UPI, Cash, Card, Bank Transfer)
- Track pending renewals and overdue payments
- Full payment history per member

**DB Tables:** `Payments`

---

### 7. Communication Engine
- Unified messaging API (WhatsApp + SMS + Email)
- Template-based messaging with variables (`{Name}`, `{ExpiryDate}`, etc.)
- Delivery tracking (Delivered / Failed / Pending)
- Templates manageable by gym owner (no code)

**DB Tables:** `MessageTemplates`, `MessageLogs`

---

### 8. Review Automation
- Automated Google review requests after 30 days of membership
- Click tracking on review links
- Post-membership engagement

**DB Tables:** `ReviewRequests`

---

### 9. Dashboard & Analytics
- KPI cards: Total Members · Revenue · Leads · Renewals Due
- Charts: Member growth (bar) · Plan mix (donut) · Revenue (line) · Lead funnel
- Best performing months table

**Scalability:** Indexed queries by `TenantId` · Materialized views for heavy reports · Read replica support planned

---

### 10. Staff & Roles Management
- Invite staff via Email / WhatsApp
- Toggle staff access (enable / disable)
- Visual permissions matrix for non-tech owners

---

### 11. Audit & Compliance
- Entity-level change tracking (created / updated / deleted / login / sent)
- Admin-level visibility across all actions
- Filter by user, action type, date range

**DB Tables:** `AuditLogs`

---

## Design Prototype

**File:** `Gym_SaaS_Project/design-prototype.html`
**Open:** Simply open in any browser — no server needed

### Screens Available

| Screen | How to Access |
|---|---|
| Landing Page | Default view on open |
| Login | "Log In" or "Start Free Trial" on landing |
| Dashboard | After login · shows KPIs, charts, alerts |
| Members | Sidebar → Members |
| Member Profile | Click any member card |
| Leads (Kanban) | Sidebar → Leads |
| Automation Rules | Sidebar → Automation |
| Payments | Sidebar → Payments |
| Membership Plans | Sidebar → Membership Plans |
| Staff & Roles | Sidebar → Staff & Roles |
| Templates & Messages | Sidebar → Templates & Messages |
| Google Reviews | Sidebar → Google Reviews |
| Reports & Analytics | Sidebar → Reports |
| Settings | Sidebar → Settings |
| Audit Logs | Sidebar → Audit Logs |

### Interactive Features in Prototype

| Feature | Detail |
|---|---|
| Notification dropdown | Bell icon — 4 unread, click to navigate |
| User dropdown | Avatar/name — profile, switch gym, logout |
| Global search | `⌘K` or search bar — live results across all data |
| Add Member modal | Full form with plan, payment, trainer fields |
| Add Lead modal | Source, interest, assign-to fields |
| Create Plan modal | Name, duration, price |
| Confirm dialog | Shown for destructive (danger zone) actions |
| Toast notifications | Every action gives contextual feedback |
| Filter tabs | Members page — All / Active / Expiring / Expired |
| Toggle switches | Automation rules, notification preferences |
| Tab system | Member profile (5 tabs), Settings (5 panels) |
| Keyboard shortcuts | `⌘K` search · `⌘N` add member · `ESC` close |

---

## Pricing (Prototype Values)

| Plan | Price | Members | Target |
|---|---|---|---|
| Starter | ₹999/month | Up to 100 | Small gyms |
| Growth | ₹2,499/month | Up to 500 | Scaling gyms |
| Enterprise | Custom | Unlimited | Chains / Large |

---

## Sample Data Used in Prototype

**Gym:** PowerFit Gym, Pune
**Owner:** Rajesh Kumar
**Members (sample):** Amit Sharma, Priya Nair, Kavita Singh, Suresh Joshi, Meena Rao
**Leads:** Rohit Verma, Sunita Gupta, Anil Kapoor, Pooja Desai, Vikram Singh
**Staff:** Priya Mehta (Manager), Ajay Kumar (Trainer), Neha Sharma (Trainer), Rahul Desai (Front Desk)

---

## Next Steps (Implementation Order Suggested)

1. **Backend Setup** — .NET 8 solution, Clean Architecture folder structure, PostgreSQL connection
2. **Multi-Tenant Foundation** — Tenant registration, JWT auth, RBAC middleware
3. **Core Domain** — Members, Memberships, Plans entities + CRUD APIs
4. **Lead Engine** — Lead model, status transitions, activity log
5. **Automation Engine** — Rule engine, Hangfire jobs, WhatsApp Cloud API integration
6. **Payment Tracking** — Payment recording, overdue detection
7. **Communication Engine** — Template system, message dispatch, delivery tracking
8. **Frontend (React + TypeScript)** — Implement the design from prototype
9. **Dashboard & Reports** — Analytics queries, chart data APIs
10. **Review Automation** — Google review link generation, click tracking
11. **Audit Logs** — Entity interceptors for change tracking
12. **DevOps** — Docker, CI/CD (GitHub Actions), cloud deployment

---

## File Structure

```
Gym_SaaS_Project/
├── PROJECT.md                              ← This file
├── Gym_SaaS_Technical_Documentation.docx  ← Original product spec
└── design-prototype.html                  ← Full interactive UI prototype
```

---

## Notes for Future Sessions

- The design prototype is a **single HTML file** with no dependencies other than CDN links (Font Awesome, Chart.js, Google Fonts) — it works fully offline if CDNs are cached.
- All sample data is India-specific (₹ currency, Indian names, WhatsApp-first messaging).
- The prototype uses **event delegation** for global interactions (no inline onclick on every element) — keep this pattern when implementing.
- Automation rules use a **trigger → condition → action** model — this is the core of the product's value proposition.
- Non-tech friendliness is a **primary design constraint** — plain language everywhere, no jargon, large click targets, helpful empty states.
