using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;
using WebApi.Graphql;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var appSettingsPath = Directory.GetCurrentDirectory();

        // Application services
        builder.Services.AddScoped<GetAllUsers>();

        // Infrastructure services
        builder.Services.AddSingleton<IUserRepository, UserRepository>();


        // WebApi services
        builder.Services.AddGraphQLServer().AddQueryType<UserQuery>();

        var app = builder.Build();

        app.MapGraphQL();

        app.Run();
    }
}
