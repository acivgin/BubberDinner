using BuuberDinner.Application.Common.Interfaces.Authentication;
using BuuberDinner.Application.Common.Interfaces.Persistance;
using BuuberDinner.Application.Common.Interfaces.Services;
using BuuberDinner.Infrastructure.Authentication;
using BuuberDinner.Infrastructure.Persistance;
using BuuberDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuuberDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
