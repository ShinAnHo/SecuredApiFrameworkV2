using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Domain.Api.Security
{
    public static class ApiHttpRequest
    {
        private static List<ApiClaim> GetTokenClaims(HttpRequest request)
        {
            var claims = new List<ApiClaim>();

            try
            {
                var authorization = request.Headers["Authorization"].ToString().Split(' ');

                if (authorization.Count() >= 2)
                {
                    var token = authorization[1];
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jsonToken = tokenHandler.ReadToken(token);
                    var securityToken = jsonToken as JwtSecurityToken;

                    foreach (Claim claim in securityToken.Claims)
                    {
                        claims.Add(new ApiClaim() { Type = claim.Type, Value = claim.Value });
                    }
                }
            }
            catch { }

            return claims;
        }
        public static string GetClaim(this HttpRequest request, string Type)
        {
            var claims = GetTokenClaims(request);

            if (!claims.Any(c => c.Type == Type))
                return "";
            else
                return claims.Where(c => c.Type == Type).First().Value;
        }
        public static List<ApiClaim> GetClaims(this HttpRequest request)
        {
            return GetTokenClaims(request);
        }
    }
}
