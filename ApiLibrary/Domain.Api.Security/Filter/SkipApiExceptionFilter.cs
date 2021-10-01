using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Domain.Api.Security
{
    public class SkipApiExceptionFilter : Attribute, IFilterMetadata { }
}
