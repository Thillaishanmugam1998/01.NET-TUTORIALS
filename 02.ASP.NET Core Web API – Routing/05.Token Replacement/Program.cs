
namespace Token_Replacement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // We only need controllers for this sample.
            // No service layer is used here.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // This project uses attribute routing.
            app.MapControllers();

            app.Run();
        }
    }
}
