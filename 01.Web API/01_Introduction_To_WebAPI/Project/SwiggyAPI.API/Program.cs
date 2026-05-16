using SwiggyAPI.API.Services;


var builder = WebApplication.CreateBuilder(args);
// Creates a WebApplicationBuilder to configure the app (handles DI, logging, config, etc.)

// Add services to the container.
builder.Services.AddControllers();
// Registers MVC controllers in DI container (to handle HTTP requests and API endpoints)

builder.Services.AddApplicationServices();
// Custom extension method - registers Swiggy-specific services (OrderService, PaymentService, etc.)

var app = builder.Build();
// Builds the final WebApplication instance with all registered services

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
// Redirects all HTTP requests to HTTPS (for security)

app.UseAuthorization();
// Enables authorization middleware (checks if user has permission to access endpoints)

app.MapControllers();
// Maps controller actions to route endpoints (makes [HttpGet], [Route], etc. work)

app.Run();
// Starts the web application and begins listening for incoming HTTP requests