using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Graphql;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Application services
        builder.Services.AddScoped<GetAllUsers>();

        // Infrastructure services
        builder.Services.AddSingleton<IUserRepository, UserRepository>();


        // WebApi services
        builder.Services.AddGraphQLServer().AddQueryType<UseQuery>();

        var app = builder.Build();

        app.MapGraphQL();

        app.Run();
    }
}
