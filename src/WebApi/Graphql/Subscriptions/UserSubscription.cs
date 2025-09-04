using Domain.Entities;
using HotChocolate;
using HotChocolate.Subscriptions;

namespace WebApi.Graphql.Subscriptions;

public class UserSubscription
{
    [GraphQLDescription("Subscribe to user creation events")]
    [Subscribe]
    public User UserCreated([EventMessage] User user) => user;
    public User UserUpdated([EventMessage] User user) => user;
    public User UserDeleted([EventMessage] User user) => user;
}
