# Post Management API

Backend service for the blog post management system built with .NET 8.

## Features

- RESTful API endpoints for blog post management
- PostgreSQL database integration using Entity Framework Core
- JWT authentication
- File upload support for blog images
- Swagger documentation
- Docker support

## Project Structure

- `Controllers/` - API endpoints
- `Data/` - Database context and models
- `Repositories/` - Data access layer
- `Middlewares/` - Custom middleware components
- `Extensions/` - Extension methods
- `Images/` - Storage for uploaded images

## Configuration

Key configuration files:
- `appsettings.json` - Main configuration file
- `Dockerfile` - Container configuration
- `Program.cs` - Application startup and DI configuration

## Development

1. Update database connection string in `appsettings.json`
2. Run migrations:
```bash
dotnet ef database update
```

3. Start the API:
```bash
dotnet run
```

The API will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
