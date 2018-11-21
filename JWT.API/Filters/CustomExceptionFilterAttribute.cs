using System.Net;
using FluentValidation;
using JWT.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JWT.API.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException exception)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                context.Result = new JsonResult(
                    exception.Errors);
                return;
            }

            var code = HttpStatusCode.InternalServerError;

            if (context.Exception is InvalidRegisterException)
            {
                code = HttpStatusCode.BadRequest;
            }
            else if (context.Exception is AccountAlreadyExistsException)
            {
                code = HttpStatusCode.Conflict;
            }
            else if (context.Exception is AccountLockedException)
            {
                code = HttpStatusCode.Locked;
            }
            else if (context.Exception is InvalidCredentialException)
            {
                code = HttpStatusCode.BadRequest;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int) code;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace
            });
        }
    }
}