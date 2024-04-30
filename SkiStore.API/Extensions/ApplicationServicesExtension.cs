﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiStore.API.Errors;
using SkiStore.Data;
using SkiStore.Data.Helper;
using SkiStore.Data.Repositories;
using SkiStore.Domain.Contracts;
using StackExchange.Redis;

namespace SkiStore.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            #region IOC
            services.AddDbContext<SkiStoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.EnableRetryOnFailure(maxRetryCount: 3
                        , maxRetryDelay: TimeSpan.FromSeconds(5)
                        , errorNumbersToAdd: null);
                    sqlServerOptionsAction.CommandTimeout(30);
                }
                );
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.LogTo(Console.WriteLine, LogLevel.Information);
                if (environment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            }
            );
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            #endregion
            #region Redis
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });
            #endregion
            #region AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));
            #endregion
            #region Override APIController Attribute Behavior
            // I Don't like it till 
            // it flat error that return at errors array
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var error = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                     .SelectMany(e => e.Value.Errors)
                     .Select(e => e.ErrorMessage).ToArray();
                    var errorResponse = new APIValidationErrorResponse(error);
                    return new BadRequestObjectResult(errorResponse);
                };

            }
            );
            #endregion
            #region CORS
            services.AddCors(setupAction => setupAction.AddPolicy("AllowAll", options =>
            options.AllowAnyHeader().AllowAnyMethod().WithOrigins(configuration["ClientOrgin"])));
            #endregion
            return services;
        }
    }
}
