using Domain.Database;

namespace Domain.IdentityServer.Oauth
{
    public class RefreshTokenRepository : RepoSqlSrvDbRepository<RefreshToken>
    {
        public RefreshTokenRepository(IUnitOfWork uow) : base(uow) { }
    }
}
