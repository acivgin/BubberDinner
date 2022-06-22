using BuuberDinner.Domain.Entities;

namespace BuuberDinner.Application.Services.Authentication;
public record AuthenticationResult(User user, string Token);