using Domain.Api;
using Domain.Database;
using RepoDb.Attributes;

namespace Domain.IdentityServer.Oauth
{
    public class ApiResource : BaseModel
    {
        [Primary]
        public long ResourceId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }
}
