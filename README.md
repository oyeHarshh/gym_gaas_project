# Redwood Labs — Gym SaaS Platform

> Membership renewal & lead conversion automation for gym owners · India-first · WhatsApp-native

---

## What Is This?

Redwood Labs is a **multi-tenant SaaS platform** built for gym owners in India. It automates the two biggest pain points in running a gym:

1. **Members lapsing** because no one followed up on expiring memberships
2. **Leads going cold** because follow-ups are manual and inconsistent

The platform handles reminders, messages, and follow-up sequences automatically — so gym owners keep members and convert leads without extra effort.

**Target users:** Non-technical gym owners, managers, trainers, front desk staff

---

## Current Status

| Phase | Status |
|---|---|
| Technical Documentation | ✅ Complete |
| Design Prototype (13 screens) | ✅ Complete |
| Backend Implementation | 🔜 Not started |
| Frontend Implementation | 🔜 Not started |

---

## Tech Stack

| Layer | Technology |
|---|---|
| **Backend** | .NET 8 — ASP.NET Core Web API |
| **Architecture** | Clean Architecture + Domain-Driven Design |
| **Database** | PostgreSQL (JSONB for dynamic config) |
| **Caching** | Redis |
| **Background Jobs** | Hangfire → RabbitMQ/Kafka (Phase 2) |
| **Frontend** | React (TypeScript) |
| **Messaging** | WhatsApp Cloud API · SMS · SendGrid / AWS SES |
| **Auth** | JWT + RBAC + Row-level tenant isolation |
| **DevOps** | Docker · GitHub Actions · Azure / AWS |

---

## Core Modules

| # | Module | Description |
|---|---|---|
| 1 | **Multi-Tenant Management** | Gym onboarding, subscription plans (Starter / Growth / Enterprise), feature gating |
| 2 | **User & Role Management** | RBAC — Owner, Manager, Trainer, Front Desk · row-level data isolation |
| 3 | **Lead Management** | Kanban pipeline: New → Contacted → Trial → Converted → Lost |
| 4 | **Automation Engine** | Configurable trigger → condition → action rules (no code needed for gym owner) |
| 5 | **Membership Lifecycle** | Plans, assignments, expiry tracking, automated reminders at 7 / 3 / 1 days |
| 6 | **Payment Tracking** | UPI, Cash, Card, Bank Transfer · overdue detection · payment history |
| 7 | **Communication Engine** | Unified WhatsApp + SMS + Email · template variables · delivery tracking |
| 8 | **Review Automation** | Auto Google review requests after 30 days · click tracking |
| 9 | **Dashboard & Analytics** | KPI cards, member growth, revenue trends, lead funnel charts |
| 10 | **Staff & Roles** | Invite via Email/WhatsApp · visual permissions matrix |
| 11 | **Audit & Compliance** | Entity-level change tracking · filter by user, action, date |

---

## Design Prototype

**File:** `design-prototype.html` — open directly in any browser, no server needed.

### Screens

Landing · Login · Dashboard · Members · Member Profile · Leads (Kanban) · Automation Rules · Payments · Membership Plans · Staff & Roles · Templates & Messages · Google Reviews · Reports · Settings · Audit Logs

### Interactive Features

- Global search (`⌘K`) across all data
- Add Member / Lead / Plan modals with full forms
- Notification and user dropdowns
- Toast notifications on every action
- Confirm dialogs for destructive actions
- Filter tabs, toggle switches, tabbed settings
- Keyboard shortcuts: `⌘K` search · `⌘N` add member · `ESC` close
- Charts: member growth (bar), plan mix (donut), revenue (line), lead funnel

---

## Pricing (Planned)

| Plan | Price | Members |
|---|---|---|
| Starter | ₹999 / month | Up to 100 |
| Growth | ₹2,499 / month | Up to 500 |
| Enterprise | Custom | Unlimited |

---

## Implementation Roadmap

1. Backend setup — .NET 8, Clean Architecture, PostgreSQL
2. Multi-tenant foundation — tenant registration, JWT auth, RBAC middleware
3. Core domain — Members, Memberships, Plans APIs
4. Lead engine — status pipeline, activity log
5. Automation engine — rule engine, Hangfire, WhatsApp Cloud API
6. Payment tracking — recording, overdue detection
7. Communication engine — template system, dispatch, delivery tracking
8. Frontend — React + TypeScript from prototype
9. Dashboard & reports — analytics APIs, chart data
10. Review automation — Google review link generation
11. Audit logs — entity interceptors
12. DevOps — Docker, CI/CD, cloud deployment

---

## Repository Structure

```
Gym_SaaS_Project/
├── README.md
├── PROJECT.md                             ← Full technical documentation
├── design-prototype.html                  ← Interactive UI prototype (13 screens)
└── Gym_SaaS_Technical_Documentation.docx ← Original product spec
```

---

## Branding

| Property | Value |
|---|---|
| Brand | Redwood Labs |
| Primary Color | `#C1440E` |
| Font | Inter |
| Icon | `fa-seedling` |
| Design | Modern SaaS · card-based · non-tech friendly · India-focused |