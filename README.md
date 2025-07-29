# Wait Documentation
Below is a comprehensive technical documentation for the Wait repository. It covers project setup, architecture, structure, configuration, services, middleware, and contribution guidelines.


## Table of Contents

- Project Overview
- Getting Started
- Architecture
- Project Structure
- Configuration
- Dependency Injection & Services
- Middleware Pipeline
- Endpoints & Features
- Validation
- Database & Migrations
- Authentication & Authorization
- Rate Limiting
- Logging & Error Handling
- Deployment
- Contributing
- License


## Project Overview
This repository hosts Wait, a minimal ASP.NET Core web application built with:
- Minimal APIs (no MVC controllers)
- Dependency injection for services and repositories
- JWT-based authentication and authorization
- Rate limiting middleware
- FluentValidation for request validation
Its goal is to demonstrate a clean architecture approach with separation of concerns between layers.

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/CLeovison/Wait
   ```
2. Navigate to the project directory
   ```sh
   cd Wait
   ```
3. Restore dependencies
   ```sh
   dotnet restore
   ```
#### Project Structure
src
    ├───Abstract
    │   └───Results
    ├───Configurations
    ├───Contracts
    │   ├───Data
    │   ├───Request
    │   │   ├───Common
    │   │   ├───ProductRequest
    │   │   └───UserRequest
    │   └───Response
    ├───Database
    ├───Domain
    │   ├───Entities
    │   └───Validation
    │       └───UserValidation
    ├───Endpoint
    │   ├───AuthEndpoint
    │   ├───ProductEndpoint
    │   └───UserEndpoint
    ├───Extensions
    ├───Helper
    ├───Infrastructure
    │   ├───Authentication
    │   ├───Mapping
    │   └───Repositories
    │       ├───AuthRepository
    │       ├───ProductRepository
    │       └───UserRepository
    ├───Migrations
    ├───Properties
    └───Services
        ├───AuthService
        ├───CartService
        ├───ProductServices
        └───UserServices