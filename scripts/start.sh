#!/bin/bash

echo "🐳 Starting API with Docker Compose..."

# Check if Docker is running
if ! docker info > /dev/null 2>&1; then
    echo "❌ Docker is not running. Please start Docker first."
    exit 1
fi

# Build and start services
echo "📦 Building and starting services..."
docker compose -f docker/docker-compose.yml up --build

echo "✅ API is running!"
echo "🌐 API: http://localhost:8080"
echo "🗄️ Database: localhost:5433"
