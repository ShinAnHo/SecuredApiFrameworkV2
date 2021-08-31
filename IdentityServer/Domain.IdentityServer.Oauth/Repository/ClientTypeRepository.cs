using Domain.Database;

namespace Domain.IdentityServer.Oauth
{
    public class ClientTypeRepository : RepoSqlSrvDbRepository<ClientType>
    {
        public ClientTypeRepository(IUnitOfWork uow) : base(uow) { }
    }
}
