using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.Users;


public class DeleteUser(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        if (user == null || user.Deleted == true)
        {
            throw new KeyNotFoundException("User not found");
        }
        // Mark user as deleted
        user.Deleted = true;
        user.UpdatedAt = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user, cancellationToken);
        return true;
    }
}
