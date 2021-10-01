using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Domain.Api.Mvc
{
    public class SkipApiExceptionFilter : Attribute, IFilterMetadata { }
}
