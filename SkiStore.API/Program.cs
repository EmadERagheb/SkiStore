using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SkiStore.API.Extensions;
using SkiStore.API.MiddleWares;
using SkiStore.Data;
using SkiStore.Data.Identity;


namespace SkiStore.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSwaggerDocumentation();
            builder.Services.AddApplicationServices(builder.Configuration, builder.Environment);
            builder.Services.AddIdentityServices(builder.Configuration, builder.Environment);


            var app = builder.Build();
            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Content")),
            //    RequestPath = "/Content"

            //});
            // this is used to handle 404 endpoints
            app.UseMiddleware<ExceptionMiddleWare>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            // Configure the HTTP request pipeline.

            app.USeSwaggerDocumentation();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            //app.MapFallbackToController("Index", "FallBack");


            #region Insure DataBase Exists And Seeds Before Run Application
            if (app.Environment.IsProduction())
            {

                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<SkiStoreDbContext>();
                var identitycontext = services.GetRequiredService<AppIdentityDbContext>();
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {

                    await context.Database.MigrateAsync();
                    await identitycontext.Database.MigrateAsync();
                }
                catch (Exception ex)
                {

                    logger.LogError(ex, "An Error occurred During Migration");
                }
            }


            #endregion

            app.Run();
        }
    }
}
