using Domain.Database;

namespace Domain.IdentityServer.Data
{
    public class UserClaimRepository : RepoSqlSrvDbRepository<UserClaim>
    {
        public UserClaimRepository(IUnitOfWork uow) : base(uow) { }
    }
}