using Domain.Database;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.IdentityServer.Oauth
{ 
    public class ClientSecretRepository : RepoSqlSrvDbRepository<ClientSecret>
    {
        public ClientSecretRepository(IUnitOfWork uow) : base(uow) { }

        public async Task<string> GetSecret(long ClientId)
        {
            var result = await base.ReadByLambda(cs => cs.ClientId == ClientId && cs.IsActive == true);

            if (!result.Any())
                throw new OauthException(OauthErrorCode.invalid_client);
            else if (result.Count() > 1)
                throw new OauthException(OauthErrorCode.invalid_client);
            else
                return result.First().Value;
        }
    }
}
