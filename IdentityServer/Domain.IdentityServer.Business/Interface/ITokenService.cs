using Domain.IdentityServer.Data;
using System;
using System.Security.Claims;

namespace Domain.IdentityServer.Business
{
    public interface ITokenService : IDisposable
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipal(string token);
        ClaimsPrincipal Validate(string token);
        ClaimsPrincipal Validate(string token, string audience);
    }
}
