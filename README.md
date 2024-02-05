# Product Commercialization API

This repository contains an ASP.NET Core API for a product commercialization company. The API is designed to manage product data, user authentication, and authorization using JSON Web Tokens (JWT). It uses MySQL as the database and Entity Framework Core for data access. The architecture follows microservices principles.

## Features

- **Product Management**:
  - Create, read, update, and delete product information.
  - Search for products based on various criteria.
  - Manage product categories and pricing.

- **User Authentication and Authorization**:
  - Secure endpoints using JWT authentication.
  - Role-based authorization for different user types (e.g., admin, sales, customer).

## Getting Started

1. **Prerequisites**:
   - Install [.NET Core SDK](https://dotnet.microsoft.com/download).
   - Set up a MySQL database (update connection string in `appsettings.json`).

2. **Configuration**:
   - Update the following values in `appsettings.json`:
     - `"ConnectionStrings:DefaultConnection"`: Connection string for your MySQL database.
     - `"JwtSettings:SecretKey"`: Secret key for JWT token generation.
     - `"JwtSettings:Issuer"` and `"JwtSettings:Audience"`: Customize as needed.
     - `"EmailConfig"`: Customize as needed.

3. **Database Migrations**:
   - Open a terminal and navigate to the project folder.
   - Run the following commands:
     ```
     dotnet ef migrations add InitialCreate
     dotnet ef database update
     ```

4. **Run the API**:
   - Execute `dotnet run` in the terminal.
   - The API will be available at `https://localhost:5263`.

## Usage

- **Authentication**:
  - Register a new user: `POST /api/Registration` (the role for the Admin user it must be named "superadmin")
  - Log in: `POST /api/Registration/login`
  - Obtain JWT token for subsequent requests.

- **Authorization**:
  - Use the JWT token obtained during login in the `Authorization` header for protected endpoints.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
