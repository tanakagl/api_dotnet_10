using Application.Services;
using Domain.Entities;
using HotChocolate;

namespace WebApi.Graphql;

public class UserQuery(GetAllUsers getAllUsers)
{
    private readonly GetAllUsers _getAllUsers = getAllUsers;
    [GraphQLDescription("Get all active users")]
    public async Task<IEnumerable<User>> GetUsers(CancellationToken cancellationToken = default)
    {
        return await _getAllUsers.ExecuteAsync(cancellationToken);
    }
}