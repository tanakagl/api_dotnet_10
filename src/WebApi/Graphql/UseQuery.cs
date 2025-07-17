using Application.Services;
using Domain.Entities;

namespace WebApi.Graphql;

public class UseQuery(GetAllUsers getAllUsers)
{
    private readonly GetAllUsers _getAllUsers = getAllUsers;

    public IEnumerable<User> GetUsers()
    {
        return _getAllUsers.Execute();
    }
}