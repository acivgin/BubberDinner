using BuuberDinner.Application.Common.Interfaces.Authentication;
using BuuberDinner.Application.Common.Interfaces.Persistance;
using BuuberDinner.Domain.Entities;

namespace BuuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository = null)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
    {

        //1. Validate the user doesn't already exist
        if (_userRepository.GetUserByEmail(Email) is not null)
        {
            throw new Exception("User already exists");
        }

        //2. If user doesn't exist, create a new user (generate unique ID) & save to database
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Password = Password
        };

        _userRepository.AddUser(user);

        //3. Generate JWT token


        // Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
                       user,
                        token
                    );
    }



    public AuthenticationResult Login(string Email, string Password)
    {
        //1. Get user from database
        if (_userRepository.GetUserByEmail(Email) is not User user)
        {
            throw new Exception("User does not exist");
        }
        //2. Validate user exists & password is correct
        if (user.Password != Password)
        {
            throw new Exception("Invalid password");
        }

        //3. If user exists & password is correct, generate JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
             user,
            token
        );
    }

}

