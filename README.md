# Wait: ASP.NET Core Web Application Documentation
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


## Getting Started
To get a local copy up and running, follow these simple steps.

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

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
## Architecture Overview

This api applies the concept of Clean Architecture and Vertical Slice Architecture, which organize by layer with the mix of organizing by features.