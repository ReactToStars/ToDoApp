# ToDoApp

A full-stack ToDo application built with ASP.NET Core Minimal API on the backend and Angular on the frontend.

## Overview

This workspace contains:

- `ToDo.API` - ASP.NET Core Web API with PostgreSQL persistence
- `ToDo.Web` - Angular frontend application
- `docker-compose.yml` - PostgreSQL, API, and Angular frontend container setup

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

- Docker Desktop or Docker Engine with the Compose plugin

## Quick Start

Start the full application stack from the repository root:

```bash
docker compose up --build
```

Open the application at `http://localhost:4200`. The API is also available directly at `http://localhost:8080`, and its Scalar documentation is at `http://localhost:8080/scalar`.

Compose waits for PostgreSQL to become healthy before starting the API. The API applies its EF Core migrations when it starts. The Angular app sends API requests through the frontend container, so it does not need a separate browser-accessible API URL.

The PostgreSQL data is persisted in the `postgres_data` volume. To stop the containers while retaining data, run:

```bash
docker compose down
```

To remove the database volume as well, run `docker compose down --volumes`.

The database password defaults to the existing local-development value. Set `POSTGRES_PASSWORD` in the environment before starting Compose to use a different password; both PostgreSQL and the API use the same value.

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
