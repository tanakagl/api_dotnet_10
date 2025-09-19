#!/bin/bash

# Stop Debug Environment (using docker commands)
echo "🛑 Stopping Debug Environment..."

# Stop and remove debug PostgreSQL container
docker stop api_dotnet_10_db_debug 2>/dev/null
docker rm api_dotnet_10_db_debug 2>/dev/null

echo "✅ Debug environment stopped."
echo ""
echo "💡 To start debugging again, run: ./scripts/start-debug-docker.sh"