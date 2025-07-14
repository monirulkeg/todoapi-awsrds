# Project Structure

```
TodoApi/
├── .github/
│   └── workflows/
│       └── ci-cd.yml                 # GitHub Actions CI/CD pipeline (Not in use right now)
├── Scripts/
│   ├── generate-migration.ps1       # PowerShell script to generate SQL migrations
│   ├── generate-migration.sh        # Bash script to generate SQL migrations
│   ├── setup.ps1                    # PowerShell setup script
│   ├── setup.sh                     # Bash setup script
│   └── migration.sql                # Generated idempotent SQL migration script
├── TodoApi.Api/
│   ├── Controllers/
│   │   ├── HealthController.cs      # Health check endpoint
│   │   └── TodosController.cs       # Main CRUD operations for todos
│   ├── Data/
│   │   └── TodoDbContext.cs         # Entity Framework DbContext
│   ├── DTOs/
│   │   └── TodoDtos.cs              # Data Transfer Objects
│   ├── Models/
│   │   └── Todo.cs                  # Todo entity model
│   ├── Services/
│   │   ├── ITodoService.cs          # Service interface
│   │   └── TodoService.cs           # Service implementation
│   ├── Migrations/                  # EF Core migrations (auto-generated)
│   ├── Properties/
│   ├── appsettings.json             # Production configuration
│   ├── appsettings.Development.json # Development configuration
│   ├── Program.cs                   # Application entry point
│   └── TodoApi.Api.csproj          # Project file
├── TodoApi.Tests/
│   ├── UnitTest1.cs                 # Unit tests for models and DTOs
│   └── TodoApi.Tests.csproj         # Test project file
├── .dockerignore                    # Docker ignore file
├── .gitignore                       # Git ignore file
├── docker-compose.yml               # Docker Compose for local development
├── Dockerfile                       # Docker image definition
├── README.md                        # Project documentation
└── TodoApi.sln                      # Solution file
```

## Key Components

### 1. **TodoApi.Api** - Main API Project

- **Controllers**: RESTful API endpoints
- **Data**: Entity Framework DbContext and database configuration
- **DTOs**: Data Transfer Objects for API requests/responses
- **Models**: Entity models representing database tables
- **Services**: Business logic layer

### 2. **TodoApi.Tests** - Test Project

- Unit tests for models, DTOs, and business logic
- Can be extended with integration tests

### 3. **Database Configuration**

- **Local Development**: PostgreSQL in Docker container
- **Production**: AWS RDS PostgreSQL (configured via connection strings)
- **Migrations**: Code-first approach with EF Core migrations

### 4. **CI/CD Pipeline**

- **GitHub Actions**: Automated testing, building, and deployment
- **Docker**: Containerized application for consistent deployments
- **Database Migrations**: Automated for development, script-based for production

### 5. **Development Tools**

- **Docker Compose**: Local PostgreSQL and pgAdmin setup
- **Scripts**: Automated setup and migration generation
- **Health Checks**: Built-in monitoring endpoints

## Configuration Files

### Connection Strings

- **Development**: Points to local Docker PostgreSQL
- **Production**: Configured for AWS RDS PostgreSQL

### Docker

- **docker-compose.yml**: Local development environment
- **Dockerfile**: Production container image
- **.dockerignore**: Optimized build context

### CI/CD

- **GitHub Actions**: Complete pipeline from test to deployment
- **Environment Variables**: Secure configuration management
