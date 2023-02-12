using Application.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

public class NotFoundExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is not NotFoundException)
        {
            return;
        }

        var viewResult = new ViewResult
        {
            ViewName = "404",
            StatusCode = 404,
        };
        context.Result = viewResult;
        context.ExceptionHandled = true;
    }
}