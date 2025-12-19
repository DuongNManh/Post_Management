using Microsoft.EntityFrameworkCore;
using Post_Management.API.Data;
using Post_Management.API.Data.Models.Domains;

namespace Post_Management.API.Data.Seeders
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Seed Categories
            await SeedCategories(context);

            // Seed Blog Images
            await SeedBlogImages(context);

            // Seed Blog Posts
            await SeedBlogPosts(context);

            await context.SaveChangesAsync();
        }

        private static async Task SeedCategories(ApplicationDbContext context)
        {
            if (await context.Categories.AnyAsync())
                return; // Categories already exist

            var categories = new List<Category>
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Technology",
                    UrlHandle = "technology"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Programming",
                    UrlHandle = "programming"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Web Development",
                    UrlHandle = "web-development"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Mobile Development",
                    UrlHandle = "mobile-development"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "DevOps",
                    UrlHandle = "devops"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "AI & Machine Learning",
                    UrlHandle = "ai-machine-learning"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Database",
                    UrlHandle = "database"
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Cloud Computing",
                    UrlHandle = "cloud-computing"
                }
            };

            await context.Categories.AddRangeAsync(categories);
        }

        private static async Task SeedBlogImages(ApplicationDbContext context)
        {
            if (await context.BlogImages.AnyAsync())
                return; // Images already exist

            var images = new List<BlogImage>
            {
                new BlogImage
                {
                    id = Guid.NewGuid(),
                    FileName = "tech-blog-1",
                    FileExtension = ".jpg",
                    Title = "Technology Blog Header",
                    URl = "https://images.unsplash.com/photo-1518770660439-4636190af475?w=800&h=400&fit=crop",
                    DateCreated = DateTime.UtcNow
                },
                new BlogImage
                {
                    id = Guid.NewGuid(),
                    FileName = "programming-1",
                    FileExtension = ".jpg",
                    Title = "Programming Code",
                    URl = "https://images.unsplash.com/photo-1555066931-4365d14bab8c?w=800&h=400&fit=crop",
                    DateCreated = DateTime.UtcNow
                },
                new BlogImage
                {
                    id = Guid.NewGuid(),
                    FileName = "web-dev-1",
                    FileExtension = ".jpg",
                    Title = "Web Development",
                    URl = "https://images.unsplash.com/photo-1547658719-da2b51169166?w=800&h=400&fit=crop",
                    DateCreated = DateTime.UtcNow
                },
                new BlogImage
                {
                    id = Guid.NewGuid(),
                    FileName = "mobile-dev-1",
                    FileExtension = ".jpg",
                    Title = "Mobile Development",
                    URl = "https://images.unsplash.com/photo-1512941937669-90a1b58e7e9c?w=800&h=400&fit=crop",
                    DateCreated = DateTime.UtcNow
                },
                new BlogImage
                {
                    id = Guid.NewGuid(),
                    FileName = "devops-1",
                    FileExtension = ".jpg",
                    Title = "DevOps Infrastructure",
                    URl = "https://images.unsplash.com/photo-1558494949-ef010cbdcc31?w=800&h=400&fit=crop",
                    DateCreated = DateTime.UtcNow
                }
            };

            await context.BlogImages.AddRangeAsync(images);
        }

        private static async Task SeedBlogPosts(ApplicationDbContext context)
        {
            if (await context.BlogPosts.AnyAsync())
                return; // Blog posts already exist

            // Get seeded categories
            var categories = await context.Categories.ToListAsync();
            var images = await context.BlogImages.ToListAsync();

            if (!categories.Any() || !images.Any())
                return; // Need categories and images first

            var techCategory = categories.First(c => c.UrlHandle == "technology");
            var programmingCategory = categories.First(c => c.UrlHandle == "programming");
            var webDevCategory = categories.First(c => c.UrlHandle == "web-development");
            var mobileDevCategory = categories.First(c => c.UrlHandle == "mobile-development");
            var devopsCategory = categories.First(c => c.UrlHandle == "devops");
            var aiCategory = categories.First(c => c.UrlHandle == "ai-machine-learning");

            var blogPosts = new List<BlogPost>
            {
                new BlogPost
                {
                    Id = Guid.NewGuid(),
                    Title = "Getting Started with .NET Core Development",
                    ShortDescription = "Learn the fundamentals of .NET Core development and build your first web application.",
                    Content = @"# Getting Started with .NET Core Development

## Introduction
.NET Core is a free, open-source, cross-platform framework for building modern applications. In this comprehensive guide, we'll explore the fundamentals of .NET Core development.

## Key Features
- **Cross-platform**: Run on Windows, macOS, and Linux
- **High Performance**: Optimized for speed and efficiency  
- **Modern Architecture**: Built for cloud and containerized applications
- **Open Source**: Community-driven development

## Setting Up Your Environment
1. Install .NET SDK
2. Choose your IDE (Visual Studio, VS Code, JetBrains Rider)
3. Create your first project using `dotnet new`

## Best Practices
- Follow SOLID principles
- Use dependency injection
- Implement proper error handling
- Write unit tests

Start building amazing applications with .NET Core today!",
                    FeaturedImageUrl = images[0].URl,
                    UrlHandle = "getting-started-dotnet-core",
                    PublishDate = DateTime.UtcNow.AddDays(-7),
                    Author = "John Developer",
                    IsVisible = true,
                    Categories = new List<Category> { techCategory, programmingCategory }
                },
                new BlogPost
                {
                    Id = Guid.NewGuid(),
                    Title = "Modern Web Development with Angular and ASP.NET Core",
                    ShortDescription = "Build full-stack applications using Angular frontend and ASP.NET Core backend.",
                    Content = @"# Modern Web Development with Angular and ASP.NET Core

## The Perfect Stack
Combining Angular's powerful frontend capabilities with ASP.NET Core's robust backend creates an ideal development environment.

## Frontend: Angular
- **TypeScript**: Strong typing for JavaScript
- **Component-based**: Reusable UI components
- **Reactive Forms**: Powerful form handling
- **RxJS**: Reactive programming with observables

## Backend: ASP.NET Core
- **RESTful APIs**: Standard HTTP endpoints
- **Entity Framework**: ORM for database operations
- **Authentication**: JWT token-based security
- **Dependency Injection**: Clean architecture

## Integration Points
1. HTTP Client for API communication
2. Authentication tokens
3. Error handling
4. Data validation

## Deployment
- Docker containers
- Azure App Service
- Kubernetes orchestration

This combination provides enterprise-grade scalability and maintainability.",
                    FeaturedImageUrl = images[1].URl,
                    UrlHandle = "angular-aspnet-core-development",
                    PublishDate = DateTime.UtcNow.AddDays(-5),
                    Author = "Sarah Johnson",
                    IsVisible = true,
                    Categories = new List<Category> { webDevCategory, programmingCategory }
                },
                new BlogPost
                {
                    Id = Guid.NewGuid(),
                    Title = "Docker and Kubernetes: Container Orchestration Guide",
                    ShortDescription = "Master containerization with Docker and orchestration with Kubernetes for modern DevOps.",
                    Content = @"# Docker and Kubernetes: Container Orchestration Guide

## Why Containers?
Containers revolutionize application deployment by providing:
- **Consistency**: Same environment across development, testing, and production
- **Isolation**: Applications run independently
- **Portability**: Run anywhere containers are supported
- **Efficiency**: Lightweight compared to virtual machines

## Docker Fundamentals
### Images and Containers
- Images are blueprints
- Containers are running instances
- Dockerfile defines the build process

### Best Practices
- Use multi-stage builds
- Minimize layer count
- Don't run as root user
- Use specific version tags

## Kubernetes Overview
Kubernetes orchestrates containerized applications:
- **Pods**: Smallest deployable units
- **Services**: Network access to pods
- **Deployments**: Manage pod lifecycle
- **ConfigMaps**: Configuration management

## Production Deployment
1. Set up clusters
2. Configure networking
3. Implement monitoring
4. Plan disaster recovery

Container orchestration is essential for modern cloud-native applications.",
                    FeaturedImageUrl = images[2].URl,
                    UrlHandle = "docker-kubernetes-guide",
                    PublishDate = DateTime.UtcNow.AddDays(-3),
                    Author = "Mike Chen",
                    IsVisible = true,
                    Categories = new List<Category> { devopsCategory, techCategory }
                },
                new BlogPost
                {
                    Id = Guid.NewGuid(),
                    Title = "Building Mobile Apps with React Native",
                    ShortDescription = "Create cross-platform mobile applications with React Native framework.",
                    Content = @"# Building Mobile Apps with React Native

## Introduction to React Native
React Native enables building mobile apps using React and JavaScript, targeting both iOS and Android platforms.

## Key Advantages
- **Code Reusability**: Share code between platforms
- **Native Performance**: Near-native app performance
- **Hot Reloading**: Fast development cycles
- **Large Community**: Extensive ecosystem

## Getting Started
1. Install Node.js and npm
2. Install React Native CLI
3. Set up Android Studio or Xcode
4. Create your first project

## Core Components
- **View**: Basic building block
- **Text**: Display text content
- **ScrollView**: Scrollable container
- **FlatList**: Efficient lists
- **TextInput**: User input

## Navigation
React Navigation provides:
- Stack navigation
- Tab navigation
- Drawer navigation

## State Management
- React Hooks for local state
- Redux for global state
- Context API for theme/auth

## Native Modules
Access device features:
- Camera
- GPS location
- Push notifications
- Biometric authentication

React Native bridges the gap between web and mobile development.",
                    FeaturedImageUrl = images[3].URl,
                    UrlHandle = "react-native-mobile-development",
                    PublishDate = DateTime.UtcNow.AddDays(-1),
                    Author = "Anna Rodriguez",
                    IsVisible = true,
                    Categories = new List<Category> { mobileDevCategory, programmingCategory }
                },
                new BlogPost
                {
                    Id = Guid.NewGuid(),
                    Title = "Introduction to Machine Learning with Python",
                    ShortDescription = "Explore machine learning concepts and build your first ML model using Python.",
                    Content = @"# Introduction to Machine Learning with Python

## What is Machine Learning?
Machine Learning enables computers to learn and make decisions from data without explicit programming.

## Types of Machine Learning
1. **Supervised Learning**: Learn from labeled data
   - Classification (categories)
   - Regression (continuous values)

2. **Unsupervised Learning**: Find patterns in unlabeled data
   - Clustering
   - Dimensionality reduction

3. **Reinforcement Learning**: Learn through interaction and rewards

## Python ML Ecosystem
### Essential Libraries
- **NumPy**: Numerical computing
- **Pandas**: Data manipulation and analysis
- **Scikit-learn**: Machine learning algorithms
- **Matplotlib/Seaborn**: Data visualization
- **TensorFlow/PyTorch**: Deep learning

## Your First ML Project
1. **Data Collection**: Gather relevant dataset
2. **Data Preprocessing**: Clean and prepare data
3. **Model Selection**: Choose appropriate algorithm
4. **Training**: Fit model to data
5. **Evaluation**: Assess model performance
6. **Deployment**: Make model available for use

## Common Algorithms
- Linear Regression
- Decision Trees
- Random Forest
- Support Vector Machines
- Neural Networks

## Best Practices
- Start simple, iterate
- Validate with separate test data
- Handle missing values
- Feature engineering matters
- Monitor model performance

Begin your ML journey and unlock the power of data-driven insights!",
                    FeaturedImageUrl = images[4].URl,
                    UrlHandle = "machine-learning-python-intro",
                    PublishDate = DateTime.UtcNow,
                    Author = "Dr. Robert Kim",
                    IsVisible = true,
                    Categories = new List<Category> { aiCategory, programmingCategory, techCategory }
                }
            };

            await context.BlogPosts.AddRangeAsync(blogPosts);
        }
    }
}