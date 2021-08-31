using Domain.Database;
using RepoDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IdentityServer.Oauth
{
    public class ClientScopeRepository : RepoSqlSrvDbRepository<ClientScope>
    {
        public ClientScopeRepository(IUnitOfWork uow) : base(uow) { }
        public async Task<bool> CheckScope(long ClientId, string ScopeName)
        {
            var query = new StringBuilder();
            query.AppendLine("SELECT cs.* FROM ApiScope s");
            query.AppendLine("    INNER JOIN ClientScope cs ON cs.ScopeId = s.ScopeId");
            query.AppendLine("WHERE s.IsActive = 1 AND cs.IsActive = 1 AND cs.ClientId = @ClientId AND s.Name = @ScopeName");

            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("ClientId", ClientId);
            parameters.Add("ScopeName", ScopeName);

            var result = await base.ReadByQuery(query.ToString(), parameters);

            return result.Any();
        }
    }
}
