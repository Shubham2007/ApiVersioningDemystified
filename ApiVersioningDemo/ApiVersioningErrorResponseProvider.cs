using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace ApiVersioningDemo
{
    /// <summary>
    /// Custom Api Versioning Errors
    /// </summary>
    internal class ApiVersioningErrorResponseProvider : DefaultErrorResponseProvider
    {
        public override IActionResult CreateResponse(ErrorResponseContext context)
        {
            switch (context.ErrorCode)
            {
                case "UnsupportedApiVersion":
                    context = new ErrorResponseContext(
                        context.Request,
                        context.StatusCode,
                        context.ErrorCode,
                        "My custom error message.",
                        context.MessageDetail);
                    break;

                case "AmbiguousApiVersion":
                    context = new ErrorResponseContext(
                        context.Request,
                        context.StatusCode,
                        context.ErrorCode,
                        "My custom error message.",
                        context.MessageDetail);
                    break;
            }

            return base.CreateResponse(context);
        }
    }
}
