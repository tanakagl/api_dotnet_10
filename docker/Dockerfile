# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copy NuGet.Config first to override any Windows-specific settings
COPY NuGet.Config ./

# Copy solution and project files
COPY api.sln ./
COPY src/WebApi/WebApi.csproj ./src/WebApi/
COPY src/Application/Application.csproj ./src/Application/
COPY src/Domain/Domain.csproj ./src/Domain/
COPY src/Infrastructure/Infrastructure.csproj ./src/Infrastructure/
COPY src/WebApiTest/WebApiTest.csproj ./src/WebApiTest/

# Clear NuGet cache and restore dependencies
RUN dotnet nuget locals all --clear
RUN dotnet restore --no-cache

# Copy source code
COPY . .

# Build and publish
WORKDIR /src/src/WebApi
RUN dotnet restore --no-cache
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS runtime
WORKDIR /app

# Create non-root user for security
RUN useradd --create-home --shell /bin/bash --user-group appuser && chown -R appuser /app
USER appuser

# Copy published app
COPY --from=build /app/out .

# Configure ports
EXPOSE 8080
EXPOSE 8081

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "WebApi.dll"]
