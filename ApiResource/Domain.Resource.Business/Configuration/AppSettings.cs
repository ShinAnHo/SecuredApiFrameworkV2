using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Resource.Business
{
    public class AppSettings
    {
        public long ResourceId { get; set; }
        public string SwaggerDefaultClientId { get; set; }
        public string SwaggerDefaultClientSecret { get; set; }
    }
}
