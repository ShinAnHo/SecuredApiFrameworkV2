using Microsoft.AspNetCore.Mvc;

namespace Domain.Api.Mvc
{
    public class RequireClaimTypeAttribute : TypeFilterAttribute
    {
        public RequireClaimTypeAttribute(string claimType) : base(typeof(RequireClaimTypeFilter))
        {
            Arguments = new object[] { claimType };
        }
    }

}
