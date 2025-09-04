using Application.Interfaces;
using Application.Services.Users;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using WebApi.Graphql.Mutations;
using WebApi.Graphql.Subscriptions;
using WebApi.Graphql.Types.Outputs;
using HotChocolate.AspNetCore;
using HotChocolate.Execution;

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
        services.AddScoped<CreateUser>();
        services.AddScoped<UpdateUser>();
        services.AddScoped<DeleteUser>();

        // Infrastructure services
        services.AddScoped<IUserRepository, UserRepository>();

        // Register logger for repositories
        services.AddScoped(provider =>
            provider.GetService<ILoggerFactory>()!.CreateLogger<UserRepository>());

        // WebApi services
        services.AddGraphQLServer()
            .AddQueryType<Graphql.Queries.UserQuery>()
            .AddMutationType<UserMutation>()
            .AddSubscriptionType<UserSubscription>()
            .AddType<UserType>()
            .AddInMemorySubscriptions();

        var app = builder.Build();

        // Configure WebSockets for subscriptions
        app.UseWebSockets();

        // Configure GraphQL
        if (app.Environment.IsDevelopment())
        {
            app.MapGraphQL().WithOptions(new GraphQLServerOptions
            {
                Tool = { Enable = true }
            });

            // Schema inspection endpoint for development
            app.MapGet("/schema", (IServiceProvider serviceProvider) =>
            {
                var executor = serviceProvider.GetRequiredService<IRequestExecutor>();
                var schema = executor.Schema;
                return Results.Text(schema.Print(), "text/plain");
            });

        }
        else
        {
            app.MapGraphQL();
        }

        app.Run();
    }
}
