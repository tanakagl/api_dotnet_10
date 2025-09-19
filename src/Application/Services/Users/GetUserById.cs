using System;
using System.Data.Common;
using System.IO.Pipelines;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.Users;

public class GetUserById(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<User> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _userRepository.GetByIdAsync(id: id, cancellationToken: cancellationToken)
        ?? throw new InvalidOperationException("Not found this user!");
    }
}
