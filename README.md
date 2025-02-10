# BlogPost Management System

A full-stack blog management system built with .NET 8 Web API, Angular 19, and PostgreSQL, containerized with Docker.

## Project Structure

- `Post_Management.API/` - Backend API built with .NET 8
- `Post_Management.UI/` - Frontend application built with Angular 19
- `docker-compose.yml` - Docker composition for all services

## Prerequisites

- Docker Desktop
- Git

## Quick Start

1. Clone the repository:
```bash
    git clone <repository-url>
    cd Post_Management
```
2. Access to the Post_Management.API/appsetting.json
    - first time build the Application:
        - set ApplyMigration: false to true (Enable auto EF Core Migration)
    - next time build the Apllication:
        - set ApplyMigration: true to false (Using existing DB)

3. Start the application using Docker Compose:
```bash
    docker-compose up -d
```

4. Access the applications:
- Frontend: http://localhost:80
- Backend API & Swagger: http://localhost:5000/swagger
- Database: localhost:5432 (PostgreSQL)

## Architecture

The application consists of three main services:

1. **Frontend (postmanagement-ui)**
   - Angular 19 application
   - Runs on port 80
   - Features Markdown support and modern UI with Tailwind CSS

2. **Backend API (postmanagement-api)**
   - .NET 8 Web API
   - Runs on ports 5000 (HTTP) and 5001 (HTTPS)
   - Provides RESTful endpoints for blog post management

3. **Database (postmanagement-db)**
   - PostgreSQL database
   - Runs on port 5432
   - Stores blog posts, categories, and images

## Development

For local development without Docker:

1. Frontend:
```bash
    cd Post_Management.UI
    npm install
    ng serve
```

2. Backend:
```bash
    cd Post_Management.API
    dotnet run
```

## Environment Setup

1. Frontend Environment Configuration:
```bash
# Navigate to UI project
cd Post_Management.UI/src/environments

# Copy template to create environment files
cp environment.template.ts environment.ts
cp environment.template.ts environment.development.ts
cp environment.template.ts environment.production.ts
```

2. Update the environment files with your specific configurations:
   - `environment.ts` - Default environment
   - `environment.development.ts` - Development environment
   - `environment.production.ts` - Production environment

Note: Environment files are git-ignored for security. Make sure to set them up locally after cloning.

## License

This project is licensed under the Unlicense - see the [LICENSE.txt](LICENSE.txt) file for details.
