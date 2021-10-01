using System;

namespace Domain.Api.Security
{
    public class SecurityConfigurationClient : ISecurityConfiguration
    {
        public string IdentityAddress { get; set; }
        public string ResourceName { get; set; }
        public string ResourceDisplayName { get; set; }
        public string ResourceVersion { get; set; }
        public string SwaggerDefaultClientId { get; set; }
        public string SwaggerDefaultClientSecret { get; set; }
        public SecurityConfigurationClient(Action<SecurityConfiguration> Configuration)
        {
            SecurityConfiguration Data = new();
            Configuration(Data);

            this.IdentityAddress = Data.IdentityAddress;
            this.ResourceName = Data.ResourceName;
            this.ResourceDisplayName = Data.ResourceDisplayName;
            this.ResourceVersion = Data.ResourceVersion;
            this.SwaggerDefaultClientId = Data.SwaggerDefaultClientId;
            this.SwaggerDefaultClientSecret = Data.SwaggerDefaultClientSecret;
        }
    }
}
