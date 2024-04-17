
using Microsoft.EntityFrameworkCore;
using SkiStore.API.Helper;
using SkiStore.Data;

namespace SkiStore.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region IOC
            builder.Services.AddDbContext<SkiStoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.EnableRetryOnFailure(maxRetryCount: 3
                        , maxRetryDelay: TimeSpan.FromSeconds(5)
                        , errorNumbersToAdd: null);
                    sqlServerOptionsAction.CommandTimeout(30);
                }
                );
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.LogTo(Console.WriteLine, LogLevel.Information);
                if (builder.Environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            }
            );

            #endregion
            #region AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            #region Insure DataBase Exists And Seeds Before Run Application
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<SkiStoreDbContext>();
            var logger = services.GetRequiredService<ILogger<Program>>();
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {

                logger.LogError(ex, "An Error occurred During Migration");
            }


            #endregion

            app.Run();
        }
    }
}
