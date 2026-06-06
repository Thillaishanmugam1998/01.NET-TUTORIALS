namespace ProducesConsumesDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region STEP 1 - REGISTER WEB API SERVICES
            // This sample enables both JSON and XML formatters because
            // the Produces and Consumes attributes work with media types.
            //
            // JSON support:
            // Built in by default
            //
            // XML support:
            // Added explicitly through AddXmlSerializerFormatters()
            builder.Services.AddControllers()
                .AddXmlSerializerFormatters()
                .AddJsonOptions(options =>
                {
                    // Keep names as written in C# to make the sample output
                    // easier to compare in JSON and XML.
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            builder.Services.AddOpenApi();
            #endregion

            var app = builder.Build();

            #region STEP 2 - CONFIGURE THE HTTP PIPELINE
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
