using SwiggyAPI.Swagger.API.Services;


/*
 * This is a .NET 9 new feature — a built-in OpenAPI endpoint at /openapi/v1.json. 
 * //builder.Services.AddOpenApi();
 * It is a replacement for UseSwagger() + AddEndpointsApiExplorer() in newer projects. 
 * app.MapOpenApi(); => It is a replacement for UseSwaggerUI() in newer projects.
 */



var builder = WebApplication.CreateBuilder(args);
// Creates the application builder that prepares services, configuration, logging, and middleware.

// Add services to the container.
builder.Services.AddControllers();
// Registers controller support so ASP.NET Core can route HTTP requests to controller actions.

builder.Services.AddEndpointsApiExplorer();
// Helps ASP.NET Core discover API endpoint details for Swagger document generation.


builder.Services.AddSwaggerGen();
// Registers Swagger generator so we can create interactive API documentation.

builder.Services.AddApplicationServices();
// Registers our custom services and service lifetimes in Dependency Injection container.

var app = builder.Build();
// Builds the final ASP.NET Core application.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    // Generates the Swagger JSON document at runtime.

    app.UseSwaggerUI();
    // Provides Swagger UI so we can test API endpoints in the browser.
}

app.UseHttpsRedirection();
// Redirects HTTP requests to HTTPS.

app.UseAuthorization();
// Adds authorization middleware to the request pipeline.

app.MapControllers();
// Makes controller routes available as API endpoints.

app.Run();
// Starts the application.
