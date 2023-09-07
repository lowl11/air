using System.Reflection;
using Air.Middlewares;
using Aircraft.Data.Contexts;
using Aircraft.Data.Interfaces.Repositories;
using Aircraft.Data.Interfaces.Services;
using Aircraft.Repositories;
using Aircraft.Services;
using Auth.Data.Contexts;
using Auth.Data.Interfaces.Repositories;
using Auth.Data.Interfaces.Services;
using Auth.Repositories;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Air;

public static class App
{
    
    public static void AddDatabase(this IServiceCollection services, ConfigurationManager manager)
    {
        // for PostgreSQL timestamp type
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        services.AddDbContext<AircraftContext>(opts
            => opts.UseNpgsql(manager.GetConnectionString("Aircraft")));
        services.AddDbContext<AuthContext>(opts
            => opts.UseNpgsql(manager.GetConnectionString("Auth")));
    }

    public static void AddRepositories(this IServiceCollection services, ConfigurationManager manager)
    {
        // auth
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        // aircraft
        services.AddScoped<FlightRepository>();
        services.AddScoped<IFlightRepository, CacheFlightRepository>();
    }

    public static void AddServices(this IServiceCollection services, ConfigurationManager manager)
    {
        // cache
        services.AddMemoryCache();
        
        // auth
        services.AddTransient<UserAccessService>();
        services.AddTransient<IUserPasswordService, UserPasswordService>(x 
            => new UserPasswordService(manager.GetValue<string>("UserKey") ?? "")
        );
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserSessionRepository, UserSessionRepository>();
         
        // stock
        services.AddScoped<IFlightService, FlightService>();
    }

    public static void AddBasic(this IServiceCollection services)
    {
        services.AddControllers().
            AddJsonOptions(options => {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                new OpenApiInfo()
                {
                    Title = "Air API",
                    Version = "v1",
                    Description = "API for \"Air\"",
                }
            );
            
            // c.OperationFilter<UserAuthAttribute>();
            
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Aircraft.xml"));
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Auth.xml"));
        });
        
        // json parsing
        services.Configure<MvcOptions>(options =>
        {
            options.ModelMetadataDetailsProviders.Add(
                new SystemTextJsonValidationMetadataProvider());
        });
    }
    
    public static void UseHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks();
    }

    public static void UseMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseMiddleware<UserSessionMiddleware>();
    }

    public static void ConfigureCors(this WebApplication app)
    {
        app.UseCors(builder => builder.AllowAnyOrigin());
    }

    public static void UseHealthcheck(this WebApplication app)
    {
        app.UseHealthChecks("/health");
        app.MapHealthChecks("/health");
    }
    
}