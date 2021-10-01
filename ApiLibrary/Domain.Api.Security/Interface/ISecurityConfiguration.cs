namespace Domain.Api.Security
{
    public interface ISecurityConfiguration
    {
        string IdentityAddress { get; set; }
        string ResourceName { get; set; }
        string ResourceDisplayName { get; set; }
        string ResourceVersion { get; set; }
        string SwaggerDefaultClientId { get; set; }
        string SwaggerDefaultClientSecret { get; set; }
    }
}
