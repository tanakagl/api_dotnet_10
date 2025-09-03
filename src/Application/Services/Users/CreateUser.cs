using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.Users;

public class CreateUser(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User> ExecuteAsync(string name, string email, CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Name = name,
            Email = email
        };

        return await _userRepository.CreateAsync(user, cancellationToken) ?? throw new InvalidOperationException("Failed to create user");
    }
}
