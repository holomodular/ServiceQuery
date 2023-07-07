﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using WebApp.Database;
using WebApp.Model;

namespace WebApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomWebsite(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddRazorPages();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddOptions();
            services.AddCors();
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    // This makes enums easier to use in Swagger using name instead of int
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // Register Database PostgreSql
            var builder = new DbContextOptionsBuilder<PostgreSqlDatabaseContext>();
            builder.UseNpgsql(Configuration.GetConnectionString("PostgreSql"));
            services.Configure<DbContextOptions<PostgreSqlDatabaseContext>>(o => { o = builder.Options; });
            services.AddSingleton<DbContextOptions<PostgreSqlDatabaseContext>>(builder.Options);
            services.AddDbContext<PostgreSqlDatabaseContext>(c => { c = builder; }, ServiceLifetime.Scoped);

            // Automapper for mapping
            services.AddAutomapper();

            // Swagger for API testing
            services.AddCustomSwagger(Configuration);

            return services;
        }

        public static IServiceCollection AddAutomapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new MediaTypeApiVersionReader();
            });
            services.AddSwaggerGen(options =>
            {
                options.ResolveConflictingActions(descriptions =>
                {
                    return descriptions.First();
                });
                options.CustomSchemaIds(x => x.FullName);
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "API v1", Version = "1.0" });
                // This removes the version parameter from swagger
                options.OperationFilter<SwaggerRemoveVersionOperationFilter>();
                // This adds the version parameter for swagger
                options.DocumentFilter<SwaggerReplaceVersionDocumentFilter>();
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    return docName == apiDesc.GroupName;
                });
            });
            return services;
        }
    }
}