namespace BindingAttributesToModelProperties
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region REGISTER SERVICES
            // Adds controller support for Web API endpoints.
            builder.Services.AddControllers();

            // Adds OpenAPI support so the sample is easy to test.
            builder.Services.AddOpenApi();
            #endregion

            var app = builder.Build();

            #region CONFIGURE MIDDLEWARE
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
