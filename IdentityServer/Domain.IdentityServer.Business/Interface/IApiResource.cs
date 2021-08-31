using Domain.IdentityServer.Oauth;
using Domain.Database;
using System.Threading.Tasks;

namespace Domain.IdentityServer.Business
{
    public interface IApiResource : IBusinessLogic<ApiResource>
    {
        Task<ApiResource> GetById(long ResourceId);
    }
}
