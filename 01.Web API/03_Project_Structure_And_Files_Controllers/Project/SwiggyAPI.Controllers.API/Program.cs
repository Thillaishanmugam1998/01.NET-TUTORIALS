using SwiggyAPI.Controllers.API.Services;

var builder = WebApplication.CreateBuilder(args);
// Creates the application builder used to configure services and middleware.

// Add services to the container.
builder.Services.AddControllers();
// Registers controller support so ASP.NET Core can find our controller classes.

builder.Services.AddApplicationServices();
// Registers our custom service classes in Dependency Injection.

var app = builder.Build();
// Builds the final application object.

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
// Redirects HTTP requests to HTTPS.

app.UseAuthorization();
// Adds authorization middleware to the request pipeline.

app.MapControllers();
// Scans the Controllers folder and maps controller routes.

app.Run();
// Starts the application.
