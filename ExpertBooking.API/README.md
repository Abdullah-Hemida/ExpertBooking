# Expert Booking Web Application

This project is a full-stack Expert Booking Platform where clients can book meetings with verified experts in various fields. Built using **ASP.NET Core Web API** for the backend and designed for a future **Angular** frontend. The system includes authentication, role-based access, dashboards, and file upload features.

---

## 📌 Features

### ✅ Authentication & Authorization
- Email & Password login and registration
- Google Login using token verification
- JWT Access & Refresh tokens with automatic renewal
- Role-based access: `Admin`, `Expert`, `Client`
- Multiple roles per user

### 👤 User Flow
- **Registration** → Role Selection → Profile Completion
- Clients can immediately use services after registration
- Experts must complete a profile and await admin approval

### 📂 File Uploads
- Profile pictures
- Identification documents
- Certifications
- Expert intro videos

### 📊 Dashboards
#### Admin Dashboard
- Manage Users (Experts, Clients)
- Approve/Reject Experts
- View stats: total users/bookings/categories
- View top-rated experts and bookings per category

#### Expert Dashboard
- View & Update Profile
- Manage Schedule Slots
- View Bookings (Confirm/Reject/Add Notes)
- Upload Certifications & Videos
- View Reviews & Statistics

#### Client Dashboard
- View & Update Profile
- Book & Cancel appointments
- Add reviews to experts
- Track own statistics (e.g., total bookings)

---

## 📦 Technologies

- **ASP.NET Core 7.0** (Web API)
- **Entity Framework Core** (Code First)
- **Identity** (Authentication & Roles)
- **JWT** (Access & Refresh Tokens)
- **AutoMapper** (DTO ↔ Entities)
- **SQL Server** (Database)
- **Postman** (Testing APIs)

---

## 🧩 Project Structure

```plaintext
ExpertBooking/
├── API/                        # API Controllers
├── Application/               # Services and Interfaces
├── Core/
│   ├── Entities/              # Domain Models
│   ├── Enums/                 # Shared enums like UserType, BookingStatus
│   ├── Interfaces/            # Repository Interfaces
│   └── Models/                # Shared Models (e.g., BookingFilter)
├── Contracts/DTOs/            # All DTOs grouped by module
├── Infrastructure/            # Repository Implementations
└── wwwroot/                   # Stored uploaded files
