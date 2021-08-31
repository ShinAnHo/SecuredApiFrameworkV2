using Domain.Database;

namespace Domain.IdentityServer.Oauth
{
    public class ApiResourceScopeRepository : RepoSqlSrvDbRepository<ApiResourceScope>
    {
        public ApiResourceScopeRepository(IUnitOfWork uow) : base(uow) { }
    }
}
