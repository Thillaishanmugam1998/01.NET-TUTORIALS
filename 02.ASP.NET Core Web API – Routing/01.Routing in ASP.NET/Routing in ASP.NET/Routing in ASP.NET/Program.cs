namespace Routing_in_ASP.NET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region 1. Add Controller Services
            // PURPOSE : MVC Controller support register பண்றோம்
            // WHY     : இல்லாட்டி controller classes scan ஆகாது
            //           routes எதுவும் work ஆகாது
            // AFFECTS : StudentController, TeacherController, etc.
            builder.Services.AddControllers();
            #endregion

            #region 2. Add OpenAPI / Swagger Services
            // PURPOSE : API documentation generate பண்றோம்
            // WHY     : Swagger UI browser-ல காட்டணும்னா இது வேணும்
            //
            // BEFORE .NET 6 (Swashbuckle):
            //   Step 1: NuGet → Swashbuckle.AspNetCore install பண்ணணும்
            //   Step 2: builder.Services.AddEndpointsApiExplorer();
            //           → Minimal API endpoints-ஐ Swagger-க்கு expose பண்ணும்
            //   Step 3: builder.Services.AddSwaggerGen();
            //           → எல்லா Controllers, Actions scan பண்ணி
            //              OpenAPI JSON document generate பண்ணும்
            //
            // .NET 9 (Built-in):
            //   builder.Services.AddOpenApi();
            //   → Swashbuckle தேவையில்ல, built-in support
            //   → NuGet package தேவையில்ல
            builder.Services.AddOpenApi();
            #endregion

            var app = builder.Build();

            #region 3. Map OpenAPI / Swagger Middleware (Development Only)
            // PURPOSE : Swagger JSON endpoint + UI expose பண்றோம்
            // WHY     : Development-ல மட்டும் காட்டணும்
            //           Production-ல API docs expose பண்றது risky
            //
            // BEFORE .NET 6 (Swashbuckle):
            //   Step 1: app.UseSwagger();
            //           → URL: /swagger/v1/swagger.json
            //              Raw JSON document serve பண்ணும்
            //   Step 2: app.UseSwaggerUI();
            //           → URL: /swagger/index.html
            //              Browser-ல visual UI காட்டும்
            //              இங்க இருந்து API test பண்ணலாம்
            //
            // .NET 9 (Built-in):
            //   app.MapOpenApi();
            //   → URL: /openapi/v1.json
            //      JSON document மட்டும் serve பண்ணும்
            //   ⚠️ Browser UI வேணும்னா Scalar NuGet install பண்ணணும்
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            #endregion

            #region 4. HTTPS Redirection Middleware
            // PURPOSE : HTTP request-ஐ HTTPS-க்கு redirect பண்றோம்
            // WHY     : Security — data encrypted ஆகி travel ஆகும்
            // EXAMPLE : http://localhost:5000  →  https://localhost:5001
            // NOTE    : Development-ல optional
            //           Production-ல mandatory
            app.UseHttpsRedirection();
            #endregion

            #region 5. Authorization Middleware
            // PURPOSE : [Authorize] attribute போட்ட endpoints-ஐ protect பண்றோம்
            // WHY     : Unauthorized users access பண்ண முடியாம பண்றோம்
            // RETURNS : 401 Unauthorized (not logged in)
            //           403 Forbidden    (logged in but no permission)
            //
            // ⚠️ IMPORTANT ORDER:
            //   app.UseAuthentication();  ← முதல்ல இது வரணும் (who are you?)
            //   app.UseAuthorization();   ← அப்புறம் இது வரணும் (what can you do?)
            //
            // ⚠️ UseAuthentication() இல்லாம் இது alone போட்டா
            //    JWT / Cookie auth work ஆகாது
            app.UseAuthorization();
            #endregion

            #region 6. Conventional Routing
            // PURPOSE : URL pattern-ஐ Program.cs-லயே define பண்றோம்
            // PATTERN : {controller}/{action}/{id?}
            //
            // HOW IT WORKS:
            //   {controller} → Controller class பேரு (suffix நீங்கும்)
            //                  StudentController → Student
            //   {action}     → Method பேரு
            //                  GetAll, Create, Edit, Delete
            //   {id?}        → Optional URL parameter
            //                  ? = வேணும்னா மட்டும் pass பண்ணு
            //
            // DEFAULT VALUES:
            //   controller default = ConventionalRouting
            //   action default     = Index
            //
            // SAMPLE URLS:
            //   GET /Student/GetAll          → GetAll() method
            //   GET /Student/GetById/5       → GetById(5) method
            //   GET /Student/Create          → Create() method
            //   PUT /Student/Edit/5          → Edit(5) method
            //   DELETE /Student/Delete/5     → Delete(5) method
            //   GET /ConventionalRouting     → default → Index()
            //
            // CONTROLLER: (No attributes needed)
            //   public class StudentController : ControllerBase
            //   {
            //       public string GetAll() { ... }
            //       public string GetById(int id) { ... }
            //       public string Create() { ... }
            //       public string Edit(int id) { ... }
            //       public string Delete(int id) { ... }
            //   }
            //
            // ⚠️ NOT RECOMMENDED for Web API
            //    MVC with Views-க்கு மட்டும் use பண்ணுவாங்க
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=ConventionalRouting}/{action=Index}/{id?}");
            #endregion

            #region 7. Attribute Routing
            // PURPOSE : Route pattern-ஐ Controller-லயே define பண்றோம்
            // WHY     : Program.cs touch பண்ணாம individual routes control பண்ணலாம்
            //
            // HOW IT WORKS:
            //   இந்த line எல்லா Controllers scan பண்ணும்
            //   [ApiController] + [Route] + [HttpGet/Post/Put/Delete]
            //   attributes படிச்சு automatically routes register பண்ணும்
            //
            // SAMPLE URLS:
            //   GET    /api/student          → GetAll()
            //   GET    /api/student/5        → GetById(5)
            //   POST   /api/student          → Create()
            //   PUT    /api/student/5        → Edit(5)
            //   DELETE /api/student/5        → Delete(5)
            //
            // CONTROLLER: (Attributes வேணும்)
            //   [ApiController]
            //   [Route("api/[controller]")]
            //   public class StudentController : ControllerBase
            //   {
            //       [HttpGet]           public string GetAll() { ... }
            //       [HttpGet("{id}")]   public string GetById(int id) { ... }
            //       [HttpPost]          public string Create() { ... }
            //       [HttpPut("{id}")]   public string Edit(int id) { ... }
            //       [HttpDelete("{id}")] public string Delete(int id) { ... }
            //   }
            //
            // ✅ RECOMMENDED for Web API
            // ✅ REST standards follow ஆகுது
            // ✅ [ApiController] benefits:
            //    → Automatic Model Validation
            //    → Automatic 400 Bad Request
            //    → [FromBody] auto binding
            app.MapControllers();
            #endregion

            #region 8. Run Application
            // PURPOSE : Application start பண்றோம்
            // WHY     : HTTP requests listen பண்ண ஆரம்பிக்கும்
            // NOTE    : இந்த line-க்கு கீழ எதுவும் execute ஆகாது
            app.Run();
            #endregion
        }
    }
}