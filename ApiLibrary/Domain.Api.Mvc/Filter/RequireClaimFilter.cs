using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Domain.Api.Mvc
{
    public class RequireClaimFilter : IAuthorizationFilter
    {
        private readonly string _type;
        private readonly string[] _values;
        public RequireClaimFilter(string type, params string[] values)
        {
            this._type = type;
            this._values = values;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userClaims = context.HttpContext.Request.GetClaims();

            if (!userClaims.Any(uc => uc.Type == _type))
                context.Result = new UnauthorizedResult();
            else
            {
                var claims = userClaims.Where(uc => uc.Type == _type).Select(uc => uc.Value).ToArray();

                if(!_values.Intersect(claims).Any())
                    context.Result = new UnauthorizedResult();
            }
        }
    }
}
