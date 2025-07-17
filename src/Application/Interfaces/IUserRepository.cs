using Domain.Entities;

namespace Application.Interfaces;
public interface IUserRepository
{
    IEnumerable<User> GetAll();
}