using Domain.IdentityServer.Data;
using Domain.IdentityServer.Oauth;
using System.Threading.Tasks;

namespace Domain.IdentityServer.Business
{
    public interface IOauthManager
    {
        Task<string> Authorize(AuthorizationRequest data);
        Task<AuthorizationResponse> Authorize(AuthenticateRequest data, string state);
        Task<AccessTokenResponse> Token(AccessTokenRequest data);
        Task Revoke(RevokeTokenRequest data);
    }
}
