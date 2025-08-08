using Application.Interfaces;
using Application.Services;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi.Graphql;
using HotChocolate.AspNetCore;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        // Database configuration
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Application services
        services.AddScoped<GetAllUsers>();

        // Infrastructure services
        services.AddScoped<IUserRepository, UserRepository>();

        // Register logger for repositories
        services.AddScoped(provider =>
            provider.GetService<ILoggerFactory>()!.CreateLogger<UserRepository>());

        // WebApi services
        services.AddGraphQLServer().AddQueryType<UserQuery>();

        var app = builder.Build();

        // Configure GraphQL
        if (app.Environment.IsDevelopment())
        {
            app.MapGraphQL().WithOptions(new GraphQLServerOptions
            {
                Tool = { Enable = true }
            });
        }
        else
        {
            app.MapGraphQL();
        }

        app.Run();
    }
}
