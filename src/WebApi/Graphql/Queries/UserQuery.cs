using Domain.Entities;
using Application.Services.Users;
using Application.Interfaces;
using Infrastructure.Context;

namespace WebApi.Graphql.Queries;

public class UserQuery
{
    [GraphQLName("getAllUser")]
    [GraphQLDescription("Get all active users")]
    public async Task<IEnumerable<User>> GetUsers(
        [Service] IUserRepository userRepository,
        CancellationToken cancellationToken = default)
    {
        return await GetAllUsers.ExecuteAsync(userRepository, cancellationToken);
    }

    [GraphQLName("getUserById")]
    [GraphQLDescription("Get user by specified id")]
    public async Task<User?> GetUserById(
        Guid id,
        [Service] IUserRepository userRepository,
        CancellationToken cancellationToken = default)
    {
        return await Application.Services.Users.GetUserById.ExecuteAsync(id, userRepository, cancellationToken);
    }
}