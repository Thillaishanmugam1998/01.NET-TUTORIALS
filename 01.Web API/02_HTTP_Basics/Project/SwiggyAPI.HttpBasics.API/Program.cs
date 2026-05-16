using SwiggyAPI.HttpBasics.API.Services;

var builder = WebApplication.CreateBuilder(args);
// Creates a WebApplicationBuilder to configure the app (handles DI, logging, config, etc.)

// Add services to the container.
builder.Services.AddControllers();
// Registers MVC controllers in DI container (to handle HTTP requests and API endpoints)

builder.Services.AddApplicationServices();
// Registers application services like RestaurantService into Dependency Injection

var app = builder.Build();
// Builds the final WebApplication instance with all registered services

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
// Redirects HTTP requests to HTTPS

app.UseAuthorization();
// Enables authorization middleware

app.MapControllers();
// Makes controller routes available as API endpoints

app.Run();
// Starts the web application
