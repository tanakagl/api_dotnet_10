namespace WebApi.Graphql.Types.Inputs;

public record UpdateUserInput(
    Guid Id,
    string Name,
    string Email
);
