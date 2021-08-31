using Domain.Database;
using RepoDb.Attributes;
using System;

namespace Domain.IdentityServer.Oauth
{
    public class ClientType 
    {
        [Primary]
        public string Type { get; set; }
    }
}
