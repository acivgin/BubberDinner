using BuuberDinner.Application.Common.Interfaces.Persistance;
using BuuberDinner.Domain.Entities;

namespace BuuberDinner.Infrastructure.Persistance
{
    public class UserRepository : IUserRepository
    {

        public static readonly List<User> _users = new();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault<User>(u => u.Email == email);
        }
    }
}