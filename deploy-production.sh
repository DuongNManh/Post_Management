#!/bin/bash

# Production Deployment Script for ZimaOS + Portainer
# This script helps deploy the Post Management system

set -e

echo "🚀 Post Management Production Deployment"
echo "========================================"

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Check if .env file exists
if [ ! -f .env ]; then
    echo -e "${YELLOW}⚠️  Creating .env file from template...${NC}"
    cp .env.example .env
    echo -e "${RED}❌ Please edit .env file with your configuration before continuing!${NC}"
    echo "   Required variables: DB_PASSWORD, JWT_SECRET, GITHUB_REPOSITORY_OWNER"
    exit 1
fi

# Source environment variables
source .env

# Validate required environment variables
echo "🔍 Validating configuration..."

if [ -z "$GITHUB_REPOSITORY_OWNER" ]; then
    echo -e "${RED}❌ GITHUB_REPOSITORY_OWNER is not set in .env${NC}"
    exit 1
fi

if [ -z "$JWT_SECRET" ] || [ ${#JWT_SECRET} -lt 32 ]; then
    echo -e "${RED}❌ JWT_SECRET must be at least 32 characters long${NC}"
    exit 1
fi

if [ -z "$DB_PASSWORD" ]; then
    echo -e "${RED}❌ DB_PASSWORD is not set in .env${NC}"
    exit 1
fi

echo -e "${GREEN}✅ Configuration validated${NC}"

# Check if Docker is running
if ! docker info > /dev/null 2>&1; then
    echo -e "${RED}❌ Docker is not running or not accessible${NC}"
    exit 1
fi

echo -e "${GREEN}✅ Docker is running${NC}"

# Pull latest images
echo "📦 Pulling latest images..."
docker-compose -f docker-compose.production.yml pull

# Stop existing containers
echo "🛑 Stopping existing containers..."
docker-compose -f docker-compose.production.yml down

# Start new deployment
echo "🚀 Starting production deployment..."
docker-compose -f docker-compose.production.yml up -d

# Wait for services to be ready
echo "⏳ Waiting for services to be ready..."
sleep 10

# Check service health
echo "🔍 Checking service health..."

# Check if containers are running
if docker-compose -f docker-compose.production.yml ps | grep -q "Up"; then
    echo -e "${GREEN}✅ Services are running${NC}"
else
    echo -e "${RED}❌ Some services failed to start${NC}"
    docker-compose -f docker-compose.production.yml logs --tail=20
    exit 1
fi

# Display deployment information
echo ""
echo "🎉 Deployment completed successfully!"
echo "====================================="
echo "🌐 Application URL: http://localhost"
echo "🗄️  Database: PostgreSQL on port 5432"
echo "📊 Logs: docker-compose -f docker-compose.production.yml logs -f"
echo "🛑 Stop: docker-compose -f docker-compose.production.yml down"
echo ""
echo "📋 Running containers:"
docker-compose -f docker-compose.production.yml ps

echo ""
echo -e "${GREEN}🚀 Post Management is now running in production mode!${NC}"