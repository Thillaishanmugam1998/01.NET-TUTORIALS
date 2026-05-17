using SwiggyAPI.ServiceLifetimes.API.Services;

var builder = WebApplication.CreateBuilder(args);
// Creates the application builder that prepares services, configuration, logging, and middleware.

// Add services to the container.
builder.Services.AddControllers();
// Registers controller support so ASP.NET Core can route HTTP requests to controller actions.

builder.Services.AddApplicationServices();
// Registers our custom services and service lifetimes in Dependency Injection container.

var app = builder.Build();
// Builds the final ASP.NET Core application.

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
// Redirects HTTP requests to HTTPS.

app.UseAuthorization();
// Adds authorization middleware to the request pipeline.

app.MapControllers();
// Makes controller routes available as API endpoints.

app.Run();
// Starts the application.
