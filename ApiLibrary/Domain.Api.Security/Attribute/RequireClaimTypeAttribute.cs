using Microsoft.AspNetCore.Mvc;

namespace Domain.Api.Security
{
    public class RequireClaimTypeAttribute : TypeFilterAttribute
    {
        public RequireClaimTypeAttribute(string claimType) : base(typeof(RequireClaimTypeFilter))
        {
            Arguments = new object[] { claimType };
        }
    }

}
