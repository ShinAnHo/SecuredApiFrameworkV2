namespace Domain.IdentityServer.Business
{
    public class AppSettings
    {
        public long ResourceId { get; set; }
        public string AppUrl { get; set; }
        public string SaltFixed { get; set; }
        public string SaltSize { get; set; }
        public string LengthRandomString { get; set; }
        public string SwaggerDefaultClientId { get; set; }
        public string SwaggerDefaultClientSecret { get; set; }
        /// <summary>
        /// In minutes
        /// </summary>
        public long CodeTokenExpiration { get; set; }
        /// <summary>
        /// In minutes
        /// </summary>
        public long AccessTokenExpiration { get; set; }
        /// <summary>
        /// In minutes
        /// </summary>
        public long RefreshTokenExpiration { get; set; }
        /// <summary>
        /// In minutes
        /// </summary>
        public long ClientSecretExpiration { get; set; }
    }
}
