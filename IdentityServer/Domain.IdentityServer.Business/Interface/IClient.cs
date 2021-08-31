using Domain.Api;
using Domain.Database;
using Domain.IdentityServer.Oauth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Domain.IdentityServer.Business
{
    public interface IClient : IBusinessLogic<Client>
    {
        Task<Client> GetById(long Id);
        Task<Client> GetByName(string Name);
        Task<ClientSecret> Register(Client data);
    }
}
