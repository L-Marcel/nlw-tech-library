using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TechLibrary.Communication.Responses;
using TechLibrary.Exceptions;

namespace TechLibrary.Api.Filters;

public class ExceptionFilter : IExceptionFilter {
    public void OnException(ExceptionContext context) {
        if(context.Exception is TechLibraryException exception) {
            context.HttpContext.Response.StatusCode = exception.GetStatusCode().GetHashCode();
            context.Result = new ObjectResult(new ResponseErrorMessagesJson() {
                Errors = exception.GetErrorMessages()
            });
        } else {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorMessagesJson() {
                Errors = ["Unknown error"]
            });
        }
    }
}