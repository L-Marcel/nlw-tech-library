using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TechLibrary.Domain.Entities;

namespace TechLibrary.Infrastructure.Security;

public abstract class AccessToken {
    public static string Generate(User user) {
        List<Claim> claims = [new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())];
        
        SecurityTokenDescriptor description = new SecurityTokenDescriptor {
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(
                SecurityKey(),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenHandler.CreateToken(description));
    }

    public static TokenValidationParameters GetTokenValidationParameters() {
        return new TokenValidationParameters {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true, 
            IssuerSigningKey = SecurityKey()
        };                                                                               
    }

    private static SymmetricSecurityKey SecurityKey() {
        const string unsafeKeyForDevelopment = "0c0jau1reJAwT8Mi048tn2RrsJ0adb5o";
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(unsafeKeyForDevelopment));
    }
}