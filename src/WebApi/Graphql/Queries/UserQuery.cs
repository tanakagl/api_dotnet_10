using Domain.Entities;
using Application.Services.Users;

namespace WebApi.Graphql.Queries;

public class UserQuery(GetAllUsers getAllUsers, GetUserById getUserById)
{
    private readonly GetAllUsers _getAllUsers = getAllUsers;
    private readonly GetUserById _getUserById = getUserById;

    [GraphQLName("getAllUser")]
    [GraphQLDescription("Get all active users")]
    public async Task<IEnumerable<User>> GetUsers(CancellationToken cancellationToken = default)
    {
        return await _getAllUsers.ExecuteAsync(cancellationToken);
    }

    [GraphQLName("getUserById")]
    [GraphQLDescription("Get user by specifieded id")]
    public async Task<User> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _getUserById.ExecuteAsync(id, cancellationToken);
    }

}