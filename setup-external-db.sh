#!/bin/bash

# Database Setup Script for External PostgreSQL
# Run this script on your PostgreSQL server or remotely

echo "🗄️  Setting up Post Management Database on External PostgreSQL"
echo "============================================================="

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Database configuration (modify as needed)
DB_HOST=${DB_HOST:-localhost}
DB_PORT=${DB_PORT:-5432}
DB_NAME=${DB_NAME:-PostManagementDB}
DB_USER=${DB_USER:-postmgmt_user}
DB_ADMIN=${DB_ADMIN:-postgres}

echo "Database Configuration:"
echo "  Host: $DB_HOST"
echo "  Port: $DB_PORT" 
echo "  Database: $DB_NAME"
echo "  User: $DB_USER"
echo ""

# Prompt for passwords
read -s -p "Enter PostgreSQL admin password: " ADMIN_PASSWORD
echo ""
read -s -p "Enter password for new database user ($DB_USER): " DB_PASSWORD
echo ""

# Test PostgreSQL connection
echo "🔍 Testing PostgreSQL connection..."
export PGPASSWORD=$ADMIN_PASSWORD

if ! psql -h $DB_HOST -p $DB_PORT -U $DB_ADMIN -d postgres -c "SELECT 1;" > /dev/null 2>&1; then
    echo -e "${RED}❌ Cannot connect to PostgreSQL server${NC}"
    echo "Please check:"
    echo "  - PostgreSQL server is running"
    echo "  - Host and port are correct"
    echo "  - Admin credentials are valid"
    echo "  - Firewall allows connections"
    exit 1
fi

echo -e "${GREEN}✅ PostgreSQL connection successful${NC}"

# Create database
echo "📊 Creating database '$DB_NAME'..."
psql -h $DB_HOST -p $DB_PORT -U $DB_ADMIN -d postgres -c "CREATE DATABASE \"$DB_NAME\";" 2>/dev/null || echo "Database already exists"

# Create user
echo "👤 Creating database user '$DB_USER'..."
psql -h $DB_HOST -p $DB_PORT -U $DB_ADMIN -d postgres -c "CREATE USER \"$DB_USER\" WITH ENCRYPTED PASSWORD '$DB_PASSWORD';" 2>/dev/null || echo "User already exists"

# Grant permissions
echo "🔐 Granting permissions..."
psql -h $DB_HOST -p $DB_PORT -U $DB_ADMIN -d postgres -c "GRANT ALL PRIVILEGES ON DATABASE \"$DB_NAME\" TO \"$DB_USER\";"
psql -h $DB_HOST -p $DB_PORT -U $DB_ADMIN -d $DB_NAME -c "GRANT ALL ON SCHEMA public TO \"$DB_USER\";"
psql -h $DB_HOST -p $DB_PORT -U $DB_ADMIN -d $DB_NAME -c "GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO \"$DB_USER\";"
psql -h $DB_HOST -p $DB_PORT -U $DB_ADMIN -d $DB_NAME -c "GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO \"$DB_USER\";"
psql -h $DB_HOST -p $DB_PORT -U $DB_ADMIN -d $DB_NAME -c "ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT ALL ON TABLES TO \"$DB_USER\";"

# Test new user connection
echo "🧪 Testing new user connection..."
export PGPASSWORD=$DB_PASSWORD
if psql -h $DB_HOST -p $DB_PORT -U $DB_USER -d $DB_NAME -c "SELECT 1;" > /dev/null 2>&1; then
    echo -e "${GREEN}✅ New user can connect successfully${NC}"
else
    echo -e "${RED}❌ New user cannot connect${NC}"
    exit 1
fi

# Generate connection string
CONNECTION_STRING="Host=$DB_HOST;Database=$DB_NAME;Username=$DB_USER;Password=$DB_PASSWORD;Port=$DB_PORT"

echo ""
echo -e "${GREEN}🎉 Database setup completed successfully!${NC}"
echo "============================================"
echo ""
echo "📋 Configuration for your .env file:"
echo "DB_CONNECTION_STRING=\"$CONNECTION_STRING\""
echo ""
echo "📋 Or for Portainer environment variable:"
echo "Key: DB_CONNECTION_STRING"
echo "Value: $CONNECTION_STRING"
echo ""
echo -e "${YELLOW}⚠️  Save this connection string securely!${NC}"
echo ""
echo "🔧 Next steps:"
echo "1. Copy the connection string to your .env file"
echo "2. Deploy your application with docker-compose.production.yml"
echo "3. The application will automatically run database migrations"

unset PGPASSWORD