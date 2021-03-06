using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;

namespace Domain.Api.Security
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (!context.Filters.OfType<SkipApiExceptionFilter>().Any())
            {
                ApiError apiError;

                if (context.Exception is ApiException)
                {
                    ApiException ex = context.Exception as ApiException;

                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    apiError = new ApiError("ERROR", ex.Validations);
                }
                else
                {
                    context.HttpContext.Response.StatusCode = 500;
                    apiError = new ApiError();
                }

                context.Result = new JsonResult(apiError);
            }

            base.OnException(context);
        }
    }
}
