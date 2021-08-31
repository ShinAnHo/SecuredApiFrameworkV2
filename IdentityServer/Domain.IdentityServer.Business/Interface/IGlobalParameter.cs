using Domain.Api;
using Domain.Database;
using Domain.IdentityServer.Data;
using Domain.IdentityServer.Oauth;
using System.Threading.Tasks;

namespace Domain.IdentityServer.Business
{
    public interface IGlobalParameter : IBusinessLogic<GlobalParameter>
    {
        Task<GlobalParameter> GetById(string ParameterID);
    }
}
