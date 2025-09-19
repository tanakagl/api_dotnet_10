using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.Users;

public class GetAllUsers
{
    public static async Task<IEnumerable<User>> ExecuteAsync(IUserRepository userRepository, CancellationToken cancellationToken = default)
    {
        return await userRepository.GetAllAsync(cancellationToken: cancellationToken);
    }
}