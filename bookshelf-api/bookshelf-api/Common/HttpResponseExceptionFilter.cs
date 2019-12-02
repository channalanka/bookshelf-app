using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace bookshelf_api.Common
{
    public class HttpResponseExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            Console.WriteLine(context.Exception.Message);
            if (context.Exception is ApiValidationException)
            {

                // handle explicit 'Http response errors
                if (context.Exception is ApiValidationException exception)
                {
                    context.Exception = null;
                    context.Result = new ObjectResult(exception.Value)
                    {
                        StatusCode = exception.StatusCode,
                    };
                }
            }
            else if (context.Exception is UnauthorizedAccessException)
            {

                context.Exception = null;
                context.Result = new ObjectResult("Unauthorized Acess")
                {
                    StatusCode = 401,
                };
            }
            else
            {
                context.Exception = null;
                context.Result = new ObjectResult("Runtime error")
                {
                    StatusCode = 500,
                };
            }


            context.ExceptionHandled = true;
        }
    }
}
