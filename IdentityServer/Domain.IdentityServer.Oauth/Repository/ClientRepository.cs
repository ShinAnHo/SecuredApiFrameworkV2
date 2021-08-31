using Domain.Database;

namespace Domain.IdentityServer.Oauth
{
    public class ClientRepository : RepoSqlSrvDbRepository<Client>
    {
        public ClientRepository(IUnitOfWork uow) : base(uow) { }
    }
}
