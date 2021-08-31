using Domain.Database;

namespace Domain.IdentityServer.Oauth
{
    public class ApiScopeRepository : RepoSqlSrvDbRepository<ApiScope>
    {
        public ApiScopeRepository(IUnitOfWork uow) : base(uow) { }
    }
}
