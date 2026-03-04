# API Endpoints

> Update this file when: adding, modifying, or removing any API endpoint.

---

## Base URL
```
http://localhost:5160/api
```

## Authentication
All protected endpoints require:
```
Authorization: Bearer <jwt_token>
```

---

## Auth — `/api/auth`

### POST `/api/auth/register`
Register a new gym (creates Tenant + Owner user).

**Request:**
```json
{
  "gymName": "PowerFit Gym",
  "ownerName": "Rajesh Kumar",
  "email": "rajesh@powerfit.com",
  "phone": "9876543210",
  "city": "Pune",
  "password": "Test@1234"
}
```

**Response `200`:**
```json
{
  "token": "<jwt>",
  "name": "Rajesh Kumar",
  "email": "rajesh@powerfit.com",
  "role": "Owner",
  "tenantId": "<guid>",
  "gymName": "PowerFit Gym",
  "expiresAt": "2026-03-05T10:05:07Z"
}
```

**Errors:**
- `409` — Email already registered

---

### POST `/api/auth/login`
Login with email and password.

**Request:**
```json
{
  "email": "rajesh@powerfit.com",
  "password": "Test@1234"
}
```

**Response `200`:** Same as register response.

**Errors:**
- `401` — Invalid email or password

---

> Add new endpoint sections below as they are built.
