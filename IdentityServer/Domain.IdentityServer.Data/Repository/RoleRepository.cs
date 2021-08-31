using Domain.Database;

namespace Domain.IdentityServer.Data
{
    public class RoleRepository : RepoSqlSrvDbRepository<Role>
    {
        public RoleRepository(IUnitOfWork uow) : base(uow) { }
    }
}