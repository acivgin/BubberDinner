using BuuberDinner.Domain.Entities;

namespace BuuberDinner.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}