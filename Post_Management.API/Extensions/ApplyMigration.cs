using Microsoft.EntityFrameworkCore;
using Post_Management.API.Data;

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
    }
}
