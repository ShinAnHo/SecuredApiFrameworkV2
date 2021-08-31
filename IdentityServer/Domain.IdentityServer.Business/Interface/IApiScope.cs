using Domain.Database;
using Domain.IdentityServer.Oauth;
using System.Threading.Tasks;

namespace Domain.IdentityServer.Business
{
    public interface IApiScope : IBusinessLogic<ApiScope>
    {
        Task<ApiScope> GetById(long ScopeId);
    }
}
