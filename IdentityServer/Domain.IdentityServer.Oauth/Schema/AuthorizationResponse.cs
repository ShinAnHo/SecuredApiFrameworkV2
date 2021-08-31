using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IdentityServer.Oauth
{
    public class AuthorizationResponse
    {
        public string code { get; set; }
        public string state { get; set; }
    }
}
