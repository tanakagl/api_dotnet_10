#!/bin/bash

echo "🧹 Cleaning Docker resources..."

# Stop and remove containers
docker compose -f docker/docker-compose.yml down -v

# Remove dangling images
docker image prune -f

# Remove unused volumes
docker volume prune -f

echo "✅ Cleanup completed!"
