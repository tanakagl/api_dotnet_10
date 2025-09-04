using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.Users;

public class UpdateUser(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User> ExecuteAsync(Guid id, string name, string email, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user == null || user.Deleted == true)
        {
            throw new KeyNotFoundException("User not found");
        }

        user.Name = name;
        user.Email = email;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user, cancellationToken);
        return user;
    }
}
