#!/bin/bash


# Start Debug Environment - Only PostgreSQL in Docker (using docker commands)
# The API will run locally for debugging

echo "🚀 Starting Debug Environment (PostgreSQL only)..."

# Stop any existing debug container
echo "🛑 Stopping existing debug containers..."
docker stop api_dotnet_10_db_debug 2>/dev/null
docker rm api_dotnet_10_db_debug 2>/dev/null

# Create network if it doesn't exist
docker network create api_debug_network 2>/dev/null

# Start PostgreSQL for debugging
echo "🐘 Starting PostgreSQL for debugging..."
docker run -d \
  --name api_dotnet_10_db_debug \
  --network api_debug_network \
  -e POSTGRES_DB=api_dotnet_10_dev \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=123 \
  -p 5433:5432 \
  -v postgres_debug_data:/var/lib/postgresql/data \
  --restart unless-stopped \
  postgres:17

# Wait for PostgreSQL to be ready
echo "⏳ Waiting for PostgreSQL to be ready..."
sleep 10

# Test PostgreSQL connection
if docker exec api_dotnet_10_db_debug pg_isready -U postgres >/dev/null 2>&1; then
    echo "✅ PostgreSQL is ready!"
    echo ""
    echo "🔧 Debug Environment Setup:"
    echo "   - PostgreSQL: localhost:5433"
    echo "   - Database: api_dotnet_10_dev"
    echo "   - Username: postgres"
    echo "   - Password: 123"
    echo ""
    echo "📝 Next steps:"
    echo "   1. Open VS Code"
    echo "   2. Press F5 or use 'Run and Debug' to start debugging"
    echo "   3. Your API will run at https://localhost:7001"
    echo "   4. GraphQL Playground: https://localhost:7001/graphql"
    echo ""
    echo "🔍 To check PostgreSQL logs: docker logs api_dotnet_10_db_debug"
    echo "🛑 To stop debug environment: ./scripts/stop-debug-docker.sh"
else
    echo "❌ PostgreSQL failed to start. Check logs with:"
    echo "   docker logs api_dotnet_10_db_debug"
fi