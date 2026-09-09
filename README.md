# ToDoApp

A full-stack ToDo application built with ASP.NET Core Minimal API on the backend and Angular on the frontend.

## Overview

This workspace contains:

- `ToDo.API` - ASP.NET Core Web API with PostgreSQL persistence
- `ToDo.Web` - Angular frontend application
- `docker-compose.yml` - PostgreSQL database container setup

## Features

- User management
- Category management
- Todo item management
- Entity Framework Core with PostgreSQL
- AutoMapper for DTO mapping
- Repository and Unit of Work patterns
- Scalar API documentation in development
- Seeded sample data for an admin user, categories, and todo items

## Project Structure

- `ToDo.API/Program.cs` - API startup and endpoint registration
- `ToDo.API/Endpoints/` - CRUD endpoint definitions for users, categories, and todo items
- `ToDo.API/Models/` - Entity models and EF Core context
- `ToDo.API/Repositories/` - Generic repository and unit-of-work implementation
- `ToDo.Web/src/app/` - Angular application source files

## Prerequisites

- .NET SDK 10.0
- Node.js 20+ and npm
- Docker Desktop (for the PostgreSQL container)

## Quick Start

### 1. Start PostgreSQL

```bash
docker compose up -d
```

This starts a PostgreSQL container named `todo_postgres` using the configuration in `docker-compose.yml`.

### 2. Run the API

```bash
dotnet restore
cd ToDo.API
dotnet run
```

The API will use the connection string configured in `ToDo.API/appsettings.json`.

### 3. View API documentation

When running in development mode, the Scalar API reference is available at:

```text
https://localhost:<port>/scalar
```

### 4. Run the Angular frontend

```bash
cd ToDo.Web
npm install
npm start
```

The Angular app will start with the Angular CLI development server.

## API Endpoints

### Users

- `GET /api/users`
- `GET /api/users/{id}`
- `POST /api/users`
- `PUT /api/users/{id}`
- `DELETE /api/users/{id}`

### Categories

- `GET /api/categories`
- `GET /api/categories/{id}`
- `POST /api/categories`
- `PUT /api/categories/{id}`
- `DELETE /api/categories/{id}`

### Todo Items

- `GET /api/todoitems`
- `GET /api/todoitems/{id}`
- `POST /api/todoitems`
- `PUT /api/todoitems/{id}`
- `DELETE /api/todoitems/{id}`

## Database

The application uses PostgreSQL with the following default connection details:

- Database: `tododb`
- Username: `bita`
- Password: `KingBitaI`

These values are configured in `ToDo.API/appsettings.json` and mirrored in the Docker setup.

## Seeded Data

The database includes sample data for:

- Admin user (`bita@example.com`)
- Categories: `Work`, `Personal`
- Todo items related to the admin user

## Notes

- The backend is implemented with minimal API endpoints rather than controllers.
- The frontend is currently a standalone Angular app scaffold and can be extended for UI features.
- The project builds successfully with the provided .NET build task.
