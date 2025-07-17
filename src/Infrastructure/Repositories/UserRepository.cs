using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new()
    {
        new User {Id = 1, Name = "John", Email = "john@example.com"},
        new User {Id = 2, Name = "Jane", Email = "jane@example.com"},
        new User {Id = 3, Name = "Bob", Email = "bob@example.com"}
    };

    public IEnumerable<User> GetAll() => _users;
}