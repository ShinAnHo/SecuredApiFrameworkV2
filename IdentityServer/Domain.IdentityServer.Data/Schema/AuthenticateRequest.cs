using System.ComponentModel.DataAnnotations;

namespace Domain.IdentityServer.Data
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
