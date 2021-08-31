using Domain.Api;
using Domain.Database;
using Domain.IdentityServer.Data;
using System.Threading.Tasks;

namespace Domain.IdentityServer.Business
{
    public interface IUser : IBusinessLogic<User>
    {
        Task RegisterUser(User data);
        Task<User> GetById(string UserID);
    }
}
