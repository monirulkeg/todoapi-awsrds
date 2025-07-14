# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY TodoApi.Api/TodoApi.Api.csproj TodoApi.Api/
RUN dotnet restore TodoApi.Api/TodoApi.Api.csproj

# Copy source code
COPY . .
WORKDIR /src/TodoApi.Api

# Build and publish
RUN dotnet publish TodoApi.Api.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published app
COPY --from=build /app/publish .

# Set environment to Production (can be overridden in GitHub Actions)
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080
ENTRYPOINT ["dotnet", "TodoApi.Api.dll"]
