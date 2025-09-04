using Application.Services.Users;
using Domain.Entities;
using HotChocolate.Subscriptions;
using WebApi.Graphql.Subscriptions;
using WebApi.Graphql.Types.Inputs;

namespace WebApi.Graphql.Mutations;

public class UserMutation
{
    [GraphQLDescription("Add a new user")]
    public async Task<User> CreateUser(
        [GraphQLDescription("User input data")] CreateUserInput input,
        [Service] CreateUser createUser,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken = default)
    {
        var user = await createUser.ExecuteAsync(input.Name, input.Email, cancellationToken);

        // Publish event for subscription
        await eventSender.SendAsync(nameof(UserSubscription.UserCreated), user, cancellationToken);

        return user;
    }

    [GraphQLDescription("Update an existing user")]
    public async Task<User> UpdateUser(
        [GraphQLDescription("User input data")] UpdateUserInput input,
        [Service] UpdateUser updateUser,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken = default)
    {
        var user = await updateUser.ExecuteAsync(input.Id, input.Name, input.Email, cancellationToken);

        // Publish event for subscription
        await eventSender.SendAsync(nameof(UserSubscription.UserUpdated), user, cancellationToken);

        return user;
    }

    [GraphQLDescription("Delete an existing user")]
    public async Task<bool> DeleteUser(
        [GraphQLDescription("User ID")] Guid id,
        [Service] DeleteUser deleteUser,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken = default)
    {
        var result = await deleteUser.ExecuteAsync(id, cancellationToken);

        if (result)
        {
            // Publish event for subscription with just the ID
            await eventSender.SendAsync(nameof(UserSubscription.UserDeleted), id, cancellationToken);
        }

        return result;
    }
}
