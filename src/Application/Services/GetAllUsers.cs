using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class GetAllUsers(IUserRepository userRepository)
{
    private readonly IUserRepository _userRepository = userRepository;

    public IEnumerable<User> Execute()
    {
        return _userRepository.GetAll();
    }
}