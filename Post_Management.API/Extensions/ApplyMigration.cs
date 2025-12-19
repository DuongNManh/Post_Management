using Microsoft.EntityFrameworkCore;
using Post_Management.API.Data;
using Post_Management.API.Data.Seeders;

namespace Post_Management.API.Extensions
{
    public static class ApplyMigration
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                var retryCount = 0;
                const int maxRetries = 5;

                while (retryCount < maxRetries)
                {
                    try
                    {
                        context.Database.Migrate();
                        break;
                    }
                    catch (Exception ex)
                    {
                        retryCount++;
                        if (retryCount == maxRetries)
                            throw;

                        // Wait before the next retry
                        Thread.Sleep(2000 * retryCount); // Exponential backoff
                    }
                }
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
                throw;
            }
        }

        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                await DatabaseSeeder.SeedAsync(context);
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
                logger.LogInformation("Database seeding completed successfully.");
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
                // Don't throw here - seeding failures shouldn't prevent app startup
            }
        }
    }
}
