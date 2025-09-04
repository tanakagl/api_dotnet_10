using HotChocolate.Execution;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Graphql;
using WebApi.Graphql.Mutations;
using WebApi.Graphql.Subscriptions;

namespace WebApi.Utilities;

public static class SchemaExporter
{
    public static string ExportSchema(IServiceProvider serviceProvider)
    {
        var executor = serviceProvider.GetRequiredService<IRequestExecutor>();
        var schema = executor.Schema;

        return schema.Print();
    }

    public static SchemaInfo GetSchemaInfo(IServiceProvider serviceProvider)
    {
        var executor = serviceProvider.GetRequiredService<IRequestExecutor>();
        var schema = executor.Schema;

        return new SchemaInfo
        {
            QueryType = schema.QueryType?.Name,
            MutationType = schema.MutationType?.Name,
            SubscriptionType = schema.SubscriptionType?.Name,
            TypeCount = schema.Types.Count,
            DirectiveCount = schema.DirectiveTypes.Count
        };
    }
}

public record SchemaInfo(
    string? QueryType = null,
    string? MutationType = null,
    string? SubscriptionType = null,
    int TypeCount = 0,
    int DirectiveCount = 0
);
