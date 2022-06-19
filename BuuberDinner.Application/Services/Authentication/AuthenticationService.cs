using BuuberDinner.Application.Common.Interfaces.Authentication;

namespace BuuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Login(string Email, string Password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            "firstName",
            "lastName",
            Email,
            "Bearer " + Guid.NewGuid().ToString()
        );
    }

    public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
    {

        // Check if user already exists


        // Create user  (generate unique Id)


        // Create JWT Token
        Guid userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, FirstName, LastName);

        return new AuthenticationResult(
                        userId,
                        FirstName,
                        LastName,
                        Email,
                        token
                    );
    }
}

