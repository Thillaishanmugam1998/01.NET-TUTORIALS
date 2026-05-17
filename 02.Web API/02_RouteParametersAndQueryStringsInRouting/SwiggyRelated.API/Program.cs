using SwiggyRelated.API.Services;

#region 01_CreateBuilder
var builder = WebApplication.CreateBuilder(args);
#endregion

#region 02_RegisterServices
// Register MVC controller support so our API can use controller classes and routing attributes.
builder.Services.AddControllers();

// Register OpenAPI support so we can inspect and test endpoints easily during learning.
builder.Services.AddOpenApi();

// Register our service using the interface -> implementation pattern.
builder.Services.AddSingleton<IOrderService, OrderService>();
#endregion

#region 03_BuildApplication
var app = builder.Build();
#endregion

#region 04_ConfigureMiddleware
// Show OpenAPI only in development to keep the app simple and safe.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Redirect HTTP calls to HTTPS when possible.
app.UseHttpsRedirection();
#endregion

#region 05_MapEndpoints
// Map controller routes such as /api/orders and /api/orders/1.
app.MapControllers();
#endregion

#region 06_RunApplication
app.Run();
#endregion
