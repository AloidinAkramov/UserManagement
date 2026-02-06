# 👤 User Management System (ASP.NET Core MVC)

A clean and practical **User Management System** built with **ASP.NET Core MVC** and **Entity Framework Core**, designed to demonstrate real-world backend and user management functionality.

This project was developed as part of a technical task and follows production-style architecture and practices.

---

## 🌐 Live Demo

🚀 **Live Website:**  
👉 http://usermanager.runasp.net/

You can test:
- User registration
- Login / logout
- User management functionality

---

## 🚀 Features

- User registration and authentication
- Secure password hashing
- User status management (Unverified / Active / Blocked)
- User management dashboard with bulk actions
- Remote Microsoft SQL Server database
- Clean MVC architecture

---

## 🛠 Tech Stack

- **ASP.NET Core MVC**
- **Entity Framework Core**
- **Microsoft SQL Server (Remote / SQL Server 2025)**
- **Bootstrap**
- **Session-based authentication**

---

## 📸 Screenshots & Feature Overview

### 1️⃣ Database Configuration (Microsoft SQL Server)

The application uses a **remotely hosted Microsoft SQL Server** database.
This setup reflects a production-like environment rather than a local-only database.

- Remote SQL Server hosting
- Secure connection string
- Entity Framework Core integration

 ### 🔐 Unique Email Constraint (Database Level)

Email uniqueness is enforced by a **UNIQUE INDEX** on the `Email` column.
Duplicate emails are rejected directly by the database.

<img width="1915" height="1119" alt="image" src="https://github.com/user-attachments/assets/caab2e8c-6c1c-4d1f-955e-946de2ea9766" />

  
---

### 2️⃣ User Registration

New users can register using email and password.

- Passwords are hashed using SHA-256
- Email uniqueness is enforced at database level
- Users are created with **Unverified** status by default

Register Page

<img width="1913" height="987" alt="image" src="https://github.com/user-attachments/assets/d726ee11-9ec6-4deb-94b4-d911d587b88f" />

---

### 3️⃣ User Login

Registered users can log in using valid credentials.

- Incorrect credentials are rejected
- **Blocked users are not allowed to log in**
- Successful login updates the `LastLoginAt` field

Login Page
<img width="1903" height="1110" alt="image" src="https://github.com/user-attachments/assets/4c8855f8-13b8-4aff-9e87-0f76e165fbea" />
<img width="1914" height="995" alt="image" src="https://github.com/user-attachments/assets/c772db1a-5b0e-432e-95e2-d91bb2757229" />

---

### 4️⃣ Users Management Dashboard

All authenticated users have access to the users management dashboard, which displays:

- Name
- Email
- Status
- Registration date
- Last login activity

Users are sorted by last activity for better visibility.

Users List
<img width="1907" height="1103" alt="image" src="https://github.com/user-attachments/assets/bfeb85d0-bd03-431a-b576-97be79f037ed" />

---

### 5️⃣ Bulk Actions

The application supports bulk operations for efficient user management:

- Block selected users
- Unblock selected users
- Delete selected users
- Delete all unverified users at once

All actions are handled using a single form with multiple endpoints.
At this stage, all authenticated users have access to these actions.
Role-based access control can be added as a future improvement.

Bulk Actions
<img width="1912" height="1103" alt="image" src="https://github.com/user-attachments/assets/c4e00e55-3170-4372-a1fd-262a4a206e46" />
<img width="1918" height="1106" alt="image" src="https://github.com/user-attachments/assets/eca27fd7-ef93-4a15-9192-bf31f148ade1" />


---

### 6️⃣ Manual User Verification (Fake Email Confirmation)

To simulate an email confirmation process, unverified users can be manually activated.

- A **“Click to verify”** button is shown only for unverified users
- Changes user status to **Active**

User Verification
<img width="1915" height="1109" alt="image" src="https://github.com/user-attachments/assets/2a32d0e7-02c4-43de-8ef7-fa059cbe7267" />
<img width="1917" height="1117" alt="image" src="https://github.com/user-attachments/assets/2ab67a9c-1b33-42d3-a7c9-f80fd6ca66ca" />


---

## 🧩 Architecture Overview

- **Controllers** – Handle HTTP requests and responses
- **Services** – Business logic and data operations
- **Data** – Entity Framework Core DbContext
- **Models** – Domain entities and enums
- **Views** – Razor views (MVC)

This separation ensures clean, maintainable, and testable code.

---

## ✅ Task Requirements Coverage

- ✔ User registration & login  
- ✔ User blocking / unblocking  
- ✔ User deletion  
- ✔ Unverified user handling  
- ✔ Fake email confirmation allowed  
- ✔ Clean and readable codebase  

---

## 📌 Notes

- Authentication is implemented using **session-based login**
- Email confirmation is intentionally simulated (as allowed by task)
- Database-level constraints are used where appropriate
- SHA-256 is used for simplicity. In production systems,
a stronger adaptive hashing algorithm (e.g. bcrypt) is recommended.

---

## 📬 Author

**Aloidin Akramov**  
GitHub: https://github.com/AloidinAkramov  
Live demo: http://usermanager.runasp.net/
