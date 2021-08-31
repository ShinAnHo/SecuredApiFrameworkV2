﻿using RepoDb.Attributes;
using Domain.Database;
using System;

namespace Domain.IdentityServer.Oauth
{
    public class ClientScope : BaseModel
    {
        [Primary]
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long ScopeId { get; set; }
    }
}
