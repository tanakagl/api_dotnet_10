using System;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.Users;

public class GetUserById
{
    public static async Task<User?> ExecuteAsync(Guid id, IUserRepository userRepository, CancellationToken cancellationToken = default)
    {
        return await userRepository.GetByIdAsync(id: id, cancellationToken: cancellationToken);
    }
}
