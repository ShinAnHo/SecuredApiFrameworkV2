using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.IdentityServer.Oauth
{
    public class RevokeTokenRequest
    {
        public string token { get; set; }
        public string token_type_hint { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }
}
