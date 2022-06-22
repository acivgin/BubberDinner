using BuuberDinner.Domain.Entities;

namespace BuuberDinner.Application.Common.Interfaces.Persistance;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void AddUser(User user);
}