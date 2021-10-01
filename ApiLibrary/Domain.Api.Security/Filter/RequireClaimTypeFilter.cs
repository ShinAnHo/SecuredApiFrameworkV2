using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Domain.Api.Security
{
    public class RequireClaimTypeFilter : IAuthorizationFilter
    {
        private readonly string _type;
        public RequireClaimTypeFilter(string type)
        {
            this._type = type;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userClaimTypes = context.HttpContext.Request.GetClaims();

            if (!userClaimTypes.Any(uc => uc.Type == _type))
                context.Result = new UnauthorizedResult();
        }
    }
}
