using Microsoft.EntityFrameworkCore;
using SkiStore.Data.Identity;

namespace SkiStore.API.Extensions
{
    public static class IdentityServiceExtension
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"), setup =>
                {
                    setup.EnableRetryOnFailure(maxRetryCount:3,maxRetryDelay:TimeSpan.FromSeconds(5),errorNumbersToAdd:null).CommandTimeout(30);
                }).LogTo(Console.WriteLine,LogLevel.Information)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                if(environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            });
            return services;
        }
    }
}
