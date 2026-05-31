var builder = WebApplication.CreateBuilder(args);

// We only need controllers for this sample project.
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

#region Controller Mapping
// This project uses attribute routing.
// Each controller action defines its own route template.
app.MapControllers();
#endregion

app.Run();
