# 💼 Expert Booking Web API

A powerful ASP.NET Core Web API for booking online consultations with verified experts. Built with Clean Architecture, SOLID principles, and support for role-based access (Admin, Expert, Client).

---

## 🚀 Features

- ✅ JWT Authentication + Refresh Token
- ✅ Register with Email/Password or Google
- ✅ Role Selection After Registration: Expert or Client
- ✅ Expert Profile Completion with:
  - Profile image
  - Bio, job title, experience, hourly rate
  - ID document, certifications, intro video
- ✅ Admin Verification System for Experts
- ✅ Client Profile Form
- ✅ Admin, Expert, and Client Dashboards (Coming Soon)
- ✅ Category & Expertise Management
- ✅ Secure File Uploads to Local Storage (Cloud support later)

---

## 🔐 Authentication & Authorization

- Multiple roles per user
- Role-based policy authorization
- Refresh token system (no re-login after expiration)
- Google login support using email

---

## 📦 Technologies

- ASP.NET Core 8 Web API
- Entity Framework Core
- Identity & Role Management
- AutoMapper
- Clean Architecture + SOLID Principles
- Local File Storage (switchable to cloud)
- SQL Server

---

## 🛠️ Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/expert-booking-api.git
