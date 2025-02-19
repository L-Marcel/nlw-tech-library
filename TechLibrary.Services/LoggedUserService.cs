using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TechLibrary.Domain.Entities;
using TechLibrary.Exceptions;
using TechLibrary.Infrastructure.DataAccess;

namespace TechLibrary.Services;

public class LoggedUserService {
    private readonly HttpContext _httpContext;
    
    public LoggedUserService(HttpContext httpContext) {
        this._httpContext = httpContext;
    }

    public User GetLoggedUser(TechLibraryDbContext context) {
        try {
            string authorization = this._httpContext.Request.Headers.Authorization.ToString();
            string token = authorization["Bearer".Length..].Trim();
            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken securityToken = tokenHandler.ReadJwtToken(token);
            Claim claim = securityToken.Claims.First<Claim>(clamp => clamp.Type == JwtRegisteredClaimNames.Sub);
            Guid userId = Guid.Parse(claim.Value);
            User user = context.Users.First(user => user.Id == userId);
            return user;
        } catch {
            throw new UnauthorizedException();
        }
    }
}