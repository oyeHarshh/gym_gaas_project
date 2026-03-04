# Redwood Labs — Gym SaaS Project

## For AI Models — IMPORTANT
- **Always read this file first** before making any changes
- **After every change** to the project (add/update/delete), update the relevant doc in `docs/`
- Docs live in `/docs` folder — keep them in sync with actual code at all times
- Never leave docs stale — if you add an endpoint, update `docs/api.md`. If you change schema, update `docs/database.md`. If you add a service, update `docs/architecture.md`

---

## What is this project?
Multi-tenant SaaS platform for Indian gym owners — automates membership renewals, lead conversion, and member communications.

**Brand:** Redwood Labs | **Color:** #C1440E | **Market:** India-first (₹, WhatsApp, UPI)

---

## Tech Stack
| Layer | Technology |
|---|---|
| Backend | .NET 8 ASP.NET Core Web API |
| Architecture | Clean Architecture + DDD |
| Database | PostgreSQL 16 |
| Caching | Redis (not yet implemented) |
| ORM | EF Core 8 + Npgsql |
| Auth | JWT + RBAC + Row-level tenant isolation |
| Password | BCrypt |
| Frontend | React + TypeScript (not yet started) |

---

## Project Location
```
/Volumes/Harsh-SSD/PROJECT/Gym_SaaS_Project/
├── design-prototype.html              ← Full UI prototype (open in browser)
├── PROJECT.md                         ← Product spec
├── README.md
└── gym_saas_backend/                  ← .NET backend
    ├── CLAUDE.md                      ← YOU ARE HERE
    ├── docs/                          ← detailed technical docs
    │   ├── architecture.md
    │   ├── database.md
    │   ├── api.md
    │   ├── auth.md
    │   └── conventions.md
    ├── GymSaaS.sln
    ├── src/
    │   ├── GymSaaS.Core               ← DTOs, Interfaces
    │   ├── GymSaaS.Domain             ← Entities, Enums, Base classes
    │   ├── GymSaaS.Infrastructure     ← EF Core, DbContext, Migrations
    │   ├── GymSaaS.Services           ← Business logic
    │   └── GymSaaS.API                ← Controllers, Program.cs
    └── tests/
        └── GymSaaS.Tests
```

---

## How to Run

### Start PostgreSQL
```bash
pg_ctl -D /Volumes/DevEnv/data/postgresql start -l /Volumes/DevEnv/data/postgresql/pg.log
```

### Run Backend
```bash
cd /Volumes/Harsh-SSD/PROJECT/Gym_SaaS_Project/gym_saas_backend
dotnet run --project src/GymSaaS.API/GymSaaS.API.csproj --urls http://localhost:5160
```

### Run Migrations
```bash
export PATH="$PATH:/Users/harshsaxena/.dotnet/tools"
dotnet ef database update --project src/GymSaaS.Infrastructure/GymSaaS.Infrastructure.csproj --startup-project src/GymSaaS.API/GymSaaS.API.csproj
```

### Build
```bash
find . -name "._*" -delete   # ALWAYS run this first (ExFAT metadata files break build)
dotnet build GymSaaS.sln
```

---

## Dev Environment
- Mac: Apple Silicon (arm64), macOS 15.5
- SSD: /Volumes/Harsh-SSD (ExFAT — no Unix permissions)
- All tools on APFS volume: /Volumes/DevEnv
- PostgreSQL superuser: `harshsaxena` (no `postgres` role)
- App DB user: `gymsaas_user` / `gymsaas_pass`
- DB name: `gymsaas`

---

## Current Implementation Status
| Module | Status |
|---|---|
| Solution structure | ✅ Done |
| Domain entities + enums | ✅ Done |
| EF Core + PostgreSQL | ✅ Done |
| DbHelperService (generic CRUD) | ✅ Done |
| JWT Auth (register + login) | ✅ Done |
| Members CRUD | 🔜 Next |
| MembershipPlans CRUD | 🔜 Pending |
| Memberships | 🔜 Pending |
| Payments | 🔜 Pending |
| Leads | 🔜 Pending |
| Automation Engine | 🔜 Pending |
| Communication Engine | 🔜 Pending |
| Dashboard Analytics | 🔜 Pending |
| Frontend (React) | 🔜 Pending |

---

## Known Issues / Gotchas
- ExFAT drive generates `._filename.cs` macOS metadata files — always run `find . -name "._*" -delete` before building
- No `postgres` role exists — use `harshsaxena` as superuser or `gymsaas_user` for app
- `dotnet-ef` tool requires: `export PATH="$PATH:/Users/harshsaxena/.dotnet/tools"`
