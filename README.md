# GameStore API

A small game-store REST API built with .NET 8 Minimal APIs, Entity Framework Core, and SQLite.

## Features

- CRUD operations for games and genres
- SQLite persistence through Entity Framework Core
- Automatic database migration on application startup
- Seed data for 10 games and 12 genres
- Swagger/OpenAPI documentation
- Docker and Docker Compose support

## Requirements

- .NET 8 SDK

Docker is optional and only required for the containerized setup.

## Continuous integration

GitHub Actions runs the build workflow for pushes and pull requests targeting the `main` or `master` branch. The workflow installs .NET 8, restores NuGet dependencies, and builds the solution in Release configuration.

## Run locally

```bash
dotnet restore
dotnet run
```

The development launch profiles use these URLs:

- HTTP: <http://localhost:5232>
- HTTPS: <https://localhost:7168>
- Swagger UI: <http://localhost:5232/swagger>

The SQLite database is created as `GameStore.db` in the application directory. Pending EF Core migrations are applied automatically when the application starts.

## Run with Docker Compose

```bash
docker compose up --build
```

The API is then available at <http://localhost:8080>, with Swagger at <http://localhost:8080/swagger>.

The database is stored in the named `gamestore-data` Docker volume and persists across container restarts.

## API endpoints

All endpoints use JSON.

### Games

| Method | Route | Description |
| --- | --- | --- |
| GET | `/games/` | List all games |
| GET | `/games/{id}` | Get a game by ID |
| POST | `/games/` | Create a game |
| PUT | `/games/{id}` | Update a game |
| DELETE | `/games/games/{id}` | Delete a game |

Example create request:

```json
{
  "name": "Hollow Knight",
  "genreId": 6,
  "price": 14.99,
  "releaseDate": "2017-02-24"
}
```

### Genres

| Method | Route | Description |
| --- | --- | --- |
| GET | `/genres/` | List all genres |
| GET | `/genres/{id}` | Get a genre by ID |
| POST | `/genres/` | Create a genre |
| PUT | `/genres/{id}` | Update a genre |
| DELETE | `/genres/{id}` | Delete a genre |

Example create request:

```json
{
  "name": "Metroidvania"
}
```

## Database migrations

Create a new migration after changing the data model:

```bash
dotnet ef migrations add <MigrationName>
```

Apply migrations manually if needed:

```bash
dotnet ef database update
```

## Project structure

```text
Data/                 EF Core DbContext and database extensions
DTOs/                 Request and response records
Endpoints/            Minimal API endpoint mappings
Migrations/           EF Core migrations and seed data
Models/               Game and genre entities
Program.cs            Application configuration and startup
```
