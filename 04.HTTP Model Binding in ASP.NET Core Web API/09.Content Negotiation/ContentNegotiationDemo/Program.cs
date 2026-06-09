using ContentNegotiationDemo.Middleware;

namespace ContentNegotiationDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            #region STEP 1 - REGISTER CONTROLLERS AND CONFIGURE CONTENT NEGOTIATION
            // This lesson project is configured to demonstrate the most useful
            // final setup from the tutorial:
            // 1. JSON formatter enabled
            // 2. XML formatter enabled
            // 3. 406 Not Acceptable enabled for unsupported Accept headers
            //
            // Because of this setup:
            // 1. Accept: application/json -> JSON response
            // 2. Accept: application/xml  -> XML response
            // 3. Accept: text/csv         -> 406 Not Acceptable
            // 4. No Accept header         -> JSON response by default
            builder.Services.AddControllers(options =>
            {
                // Strict content negotiation:
                // If the client requests a response format we do not support,
                // ASP.NET Core returns HTTP 406 instead of silently falling back.
                options.ReturnHttpNotAcceptable = true;
            })
            .AddXmlSerializerFormatters()
            .AddJsonOptions(options =>
            {
                // Keep property names exactly as written in C# so the sample
                // output is easier to compare in JSON and XML.
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            #endregion

            #region STEP 2 - ARTICLE REFERENCE NOTES
            // If you want to test the earlier article stages, change the code above:
            //
            // A. Default JSON-only behavior:
            //    builder.Services.AddControllers();
            //
            // B. JSON + XML behavior:
            //    builder.Services.AddControllers()
            //                    .AddXmlSerializerFormatters();
            //
            // C. XML as the default formatter:
            //    builder.Services.AddControllers(options =>
            //    {
            //        var xmlFormatter = new Microsoft.AspNetCore.Mvc.Formatters.XmlSerializerOutputFormatter();
            //        options.OutputFormatters.Insert(0, xmlFormatter);
            //    }).AddXmlSerializerFormatters();
            //
            // D. Strict 406 behavior:
            //    Use the current configuration with ReturnHttpNotAcceptable = true.
            #endregion

            var app = builder.Build();

            #region STEP 3 - CONFIGURE THE HTTP PIPELINE
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // This middleware customizes the 406 response body so learners
            // can clearly see why the request failed.
            app.UseMiddleware<CustomNotAcceptableMiddleware>();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
