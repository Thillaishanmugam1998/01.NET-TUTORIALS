using SwiggyAPI.Hosting.API.Services;

var builder = WebApplication.CreateBuilder(args);
// Creates the application builder that prepares hosting, services, logging, and configuration.

// Add services to the container.
builder.Services.AddControllers();
// Registers controller support so ASP.NET Core can route HTTP requests to controller actions.

builder.Services.AddApplicationServices();
// Registers our custom business services and hosting info service into Dependency Injection.

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

// Important note:
// When this API is deployed to IIS using the in-process hosting model,
// the ASP.NET Core app runs inside the IIS worker process.
