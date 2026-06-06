using System.Text.Json;

namespace ContentNegotiationDemo.Middleware
{
    public class CustomNotAcceptableMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomNotAcceptableMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            #region CUSTOMIZE 406 NOT ACCEPTABLE RESPONSE
            // After the next middleware/controller runs, inspect the response.
            // If ASP.NET Core produced a 406 response, replace the empty/default
            // body with a clear JSON message for learning purposes.
            #endregion
            if (context.Response.StatusCode == StatusCodes.Status406NotAcceptable &&
                !context.Response.HasStarted)
            {
                var acceptHeader = context.Request.Headers.Accept.ToString();

                context.Response.ContentType = "application/json";

                var response = new
                {
                    Code = StatusCodes.Status406NotAcceptable,
                    ErrorMessage = $"The requested format '{acceptHeader}' is not supported by this API.",
                    SupportedFormats = new[]
                    {
                        "application/json",
                        "application/xml"
                    }
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
