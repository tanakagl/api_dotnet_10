using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class GetAllUsers(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<IEnumerable<User>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await _userRepository.GetAllAsync(cancellationToken: cancellationToken);
    }
}