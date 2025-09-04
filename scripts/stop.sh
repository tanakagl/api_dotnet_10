#!/bin/bash

echo "🛑 Stopping API services..."

# Stop all services
docker compose -f docker/docker-compose.yml down

echo "✅ Services stopped!"
