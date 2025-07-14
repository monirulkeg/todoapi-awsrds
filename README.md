# Todo API

A .NET 8 Web API for managing todos with PostgreSQL database, Entity Framework Core, and Docker support.

## Features

- **CRUD Operations**: Create, Read, Update, Delete todos
- **PostgreSQL Database**: Local Docker container for development, AWS RDS for production
- **Entity Framework Core**: Code-first approach with migrations
- **Docker Support**: Containerized application with docker-compose for local development
- **GitHub Actions**: CI/CD pipeline for automated testing and deployment
- **Swagger/OpenAPI**: Interactive API documentation
- **Health Checks**: Built-in health monitoring endpoints

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [PostgreSQL Client](https://www.postgresql.org/download/) (optional, for direct database access)

## Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd TodoApi
```

### 2. Start Local Database

```bash
docker-compose up -d postgres
```

This will start PostgreSQL on port 5432 with:

- Database: `todoapi`
- Username: `todouser`
- Password: `todopass`

### 3. Run the Application

```bash
cd TodoApi.Api
dotnet run
```

The API will be available at:

- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger UI: https://localhost:5001 (or http://localhost:5000)

### 4. Database Migrations

The application automatically runs migrations in development mode. For manual migration management:

```bash
# Add a new migration
dotnet ef migrations add <MigrationName>

# Update database
dotnet ef database update

# Generate SQL script for production
dotnet ef migrations script --idempotent
```

## API Endpoints

### Todos

- `GET /api/todos` - Get all todos
- `GET /api/todos/{id}` - Get a specific todo
- `POST /api/todos` - Create a new todo
- `PUT /api/todos/{id}` - Update a todo
- `DELETE /api/todos/{id}` - Delete a todo
- `GET /api/todos/status/{isCompleted}` - Get todos by completion status
- `GET /api/todos/category/{category}` - Get todos by category
- `PATCH /api/todos/{id}/complete` - Mark todo as completed
- `PATCH /api/todos/{id}/incomplete` - Mark todo as incomplete

### Health Check

- `GET /health` - Application health status

## Data Model

### Todo Entity

```json
{
  "id": 1,
  "title": "Complete project setup",
  "description": "Set up the initial .NET API project",
  "isCompleted": false,
  "createdAt": "2024-01-15T10:00:00Z",
  "updatedAt": "2024-01-15T10:30:00Z",
  "completedAt": null,
  "priority": "High",
  "category": "Development"
}
```

## Configuration

### Connection Strings

#### Development (appsettings.Development.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=todoapi;Username=todouser;Password=todopass"
  }
}
```

#### Production (Environment Variables)

```bash
ConnectionStrings__ProductionConnection="Host=your-rds-endpoint;Port=5432;Database=todoapi;Username=todouser;Password=your-secure-password"
```

### AWS RDS Configuration

For production deployment with AWS RDS PostgreSQL:

1. Update the production connection string in `appsettings.json`
2. Configure AWS credentials for the deployment environment
3. Ensure RDS security groups allow connections from your application

## Docker

### Build Docker Image

```bash
docker build -t todoapi .
```

### Run with Docker Compose

```bash
docker-compose up
```

This starts both the API and PostgreSQL database.

## GitHub Actions CI/CD

The repository includes a GitHub Actions workflow (`.github/workflows/ci-cd.yml`) that:

1. **Test**: Runs unit tests with a PostgreSQL test database
2. **Build**: Builds and pushes Docker images to Docker Hub
3. **Deploy**: Deploys to production (AWS ECS/EKS)

### Required Secrets

Configure these secrets in your GitHub repository:

- `DOCKER_USERNAME` - Docker Hub username
- `DOCKER_PASSWORD` - Docker Hub password/token
- `AWS_ACCESS_KEY_ID` - AWS access key
- `AWS_SECRET_ACCESS_KEY` - AWS secret key

## Development Tools

### pgAdmin

Access pgAdmin at http://localhost:8080 (when running docker-compose):

- Email: admin@todoapi.com
- Password: admin

### Entity Framework Tools

```bash
# Install EF CLI tools globally
dotnet tool install --global dotnet-ef

# Update tools
dotnet tool update --global dotnet-ef
```

## Production Deployment

### Database Migrations

For production, use idempotent scripts:

```bash
dotnet ef migrations script --idempotent --output migration.sql
```

Apply the script to your production database using your preferred method (psql, Azure Data Studio, etc.).

### Environment Variables

Set these environment variables in production:

```bash
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__ProductionConnection="Your RDS connection string"
```

## Monitoring and Logging

- Health checks available at `/health`
- Structured logging configured for different environments
- Database command logging enabled in development

## Troubleshooting

### Entity Framework Pending Model Changes Warning

If you encounter the error:

```
System.InvalidOperationException: An error was generated for warning 'Microsoft.EntityFrameworkCore.Migrations.PendingModelChangesWarning': The model for context 'TodoDbContext' changes each time it is built.
```

This was caused by using `DateTime.UtcNow` in seed data. This has been fixed by using static datetime values in the `TodoDbContext`. If you see this error:

1. Make sure you have the latest migrations applied:

   ```bash
   dotnet ef database update
   ```

2. If the error persists, you can suppress the warning by adding this to your `TodoDbContext.OnConfiguring`:
   ```csharp
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
       optionsBuilder.ConfigureWarnings(warnings =>
           warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
   }
   ```

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Submit a pull request

## License

This project is licensed under the MIT License.
