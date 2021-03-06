using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuuberDinner.Application.Common.Interfaces.Authentication;
using BuuberDinner.Application.Common.Interfaces.Services;
using BuuberDinner.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BuuberDinner.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> _jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = _jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Iat, _dateTimeProvider.UtcNow.ToString("yyyy-MM-dd")),
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret ?? "")),
                SecurityAlgorithms.HmacSha256);



            var securityToken = new JwtSecurityToken(
                  issuer: _jwtSettings.Issuer,
                  audience: _jwtSettings.Audience,
                  expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes),
                  claims: claims,
                  signingCredentials: signingCredentials);


            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

    }

}
