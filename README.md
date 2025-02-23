# 📚 BookManagement API

The **BookManagement API** is a .NET-based application that allows you to manage books and users. It includes features like JWT-based authentication, user registration, and book management. The project is containerized using Docker Compose for easy setup and deployment.

## 📖 Table of Contents
- [✨ Features](#features)
- [⚙️ Prerequisites](#prerequisites)
- [🚀 Setup with Docker Compose](#setup-with-docker-compose)
- [📡 API Endpoints](#api-endpoints)
- [🗄 Database Schema](#database-schema)
- [🔧 Environment Variables](#environment-variables)
- [📜 Running Migrations](#running-migrations)
- [🤝 Contributing](#contributing)
- [📄 License](#license)

## ✨ Features

### 🔑 User Authentication:
- ✅ Register new users.
- 🔐 Login with JWT token generation.

### 📚 Book Management:
- ➕ Add, ✏️ update, and ❌ delete books.
- 📖 Retrieve a list of books with pagination.

### 🐳 Docker Support:
- ⚡ Easy setup using Docker Compose.
- 🗄 Separate databases for application data and identity.

## ⚙️ Prerequisites
Before you begin, ensure you have the following installed:
- 🐳 **Docker**
- 🛠 **Docker Compose**
- 💻 **.NET SDK** (optional, for local development)

## 🚀 Setup with Docker Compose
Follow these steps to set up and run the project using Docker Compose:

### 📂 Clone the Repository:
```bash
git clone https://github.com/GioKhvedelidze/BookManagement.git
cd BookManagement
```

### 🔧 Update Environment Variables:
Open the `docker-compose.yml` file and ensure the connection strings and JWT settings are correct.
Alternatively, you can override these settings using environment variables.

### ⚙️ Build and Run the Containers:
```bash
docker-compose up --build
```
This will:
- 🏗 Build the **BookManagement.API** image.
- 🗄 Start two **SQL Server** containers: one for the **BookManagement database** and one for the **Identity database**.
- 🚀 Run the API in a container.

### 🌍 Access the API:
The API will be available at:
```
http://localhost:5000
```

## 📡 API Endpoints
| 🔄 Method | 🌐 Endpoint | 📋 Description |
|--------|---------|-------------|
| 📝 POST | `/api/auth/register` | Register a new user |
| 🔑 POST | `/api/auth/login` | Authenticate a user and get a JWT token |
| 📖 GET | `/api/books` | Get a list of books (paginated) |
| ➕ POST | `/api/books` | Add a new book |
| ✏️ PUT | `/api/books/{id}` | Update a book by ID |
| ❌ DELETE | `/api/books/{id}` | Delete a book by ID |

## 🗄 Database Schema
The API uses two separate databases:
1. 📘 **BookManagement Database** - Stores book-related data.
2. 🔐 **Identity Database** - Manages user authentication data.

## 🔧 Environment Variables
Configure the following environment variables in your `.env` file or Docker environment:
```env
CONNECTION_STRING=your_database_connection_string
IDENTITY_DB_CONNECTION=your_identity_database_connection_string
JWT_SECRET=your_jwt_secret_key
```

## 📜 Running Migrations
To apply database migrations manually:
```bash
dotnet ef database update
```
Ensure that the correct connection string is configured before running migrations.

## 🤝 Contributing
Contributions are welcome! Please follow these steps:
1. 🍴 Fork the repository.
2. 🌿 Create a new branch.
3. 🛠 Make your changes.
4. 🔄 Submit a pull request.

## 📄 License
This project is licensed under the **MIT License**. See the `LICENSE` file for details.

