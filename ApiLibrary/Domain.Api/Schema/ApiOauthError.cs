using System.Text.Json.Serialization;
using Domain.Api;
using System.ComponentModel;

namespace Domain.Api
{
    public class ApiOauthError
    {
        public string error { get; set; }
        public string error_description { get; set; }
        public string error_uri { get; set; }
    }
}
