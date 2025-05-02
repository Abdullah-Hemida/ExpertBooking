# ExpertBooking Platform 🧠💼

ExpertBooking is a robust freelance-style platform built with **ASP.NET Core Web API** and **Entity Framework Core**, allowing clients to book online meetings with verified experts based on their expertise, rates, and availability.

---

## 📌 Features

### 🔐 Authentication & Authorization
- JWT-based authentication with role-based access control (`Admin`, `Expert`, `Client`)
- Register with Email/Password or Google
- Role selection and profile completion post-registration
- Auto-refresh token system for seamless login

### 👤 User Roles

#### ✅ Admin
- Approve/reject expert registrations
- Manage users, categories, and bookings
- View platform statistics (total users/bookings/categories)
- View top-rated experts & booking stats per category

#### ✅ Expert Dashboard
- View/update expert profile
- Upload ID, certifications, and intro video
- Manage availability schedule
- View and respond to bookings (confirm/reject, add notes)
- View reviews and stats (total bookings, average rating)

#### ✅ Client Dashboard
- Complete/update client profile
- View/cancel bookings
- Add reviews to experts
- View dashboard statistics

---

## 🌐 Website (Public API)

Accessible without login for general users:

- ✅ View featured experts
- ✅ Search experts by keyword, category, rate, experience
- ✅ Browse expert public profiles
- ✅ View available booking slots
- ✅ Book appointments with available experts

---

## 📦 Tech Stack

| Layer           | Tech Used                        |
|----------------|-----------------------------------|
| Backend         | ASP.NET Core Web API             |
| Authentication  | JWT + Identity                   |
| ORM             | Entity Framework Core            |
| Mapping         | AutoMapper                       |
| Storage         | Local file storage (with option for cloud) |
| Database        | SQL Server                       |

---

## 📁 Project Structure

```bash
ExpertBooking/
│
├── ExpertBooking.API                # ASP.NET Core Web API
├── ExpertBooking.Application        # Application services, interfaces, and DTOs
├── ExpertBooking.Contracts          # Shared DTOs and Enums
├── ExpertBooking.Core               # Entities, Enums, Interfaces (Repositories)
├── ExpertBooking.Infrastructure     # EF Core DbContext, Repositories, Configs
└── README.md

