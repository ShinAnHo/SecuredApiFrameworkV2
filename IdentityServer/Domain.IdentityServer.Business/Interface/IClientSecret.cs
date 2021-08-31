using Domain.Api;
using Domain.Database;
using Domain.IdentityServer.Oauth;
using System.Threading.Tasks;

namespace Domain.IdentityServer.Business
{
    public interface IClientSecret : IBusinessLogic<ClientSecret>
    {
        Task<ClientSecret> GetById(long Id);
    }
}
