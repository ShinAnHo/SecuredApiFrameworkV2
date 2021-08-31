using RepoDb.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IdentityServer.Oauth
{
    public class AuthorizationCode
    {
        [Primary]
        public string Code { get; set; }
        public DateTime Expire { get; set; }
        public string UserId { get; set; }
    }
}
