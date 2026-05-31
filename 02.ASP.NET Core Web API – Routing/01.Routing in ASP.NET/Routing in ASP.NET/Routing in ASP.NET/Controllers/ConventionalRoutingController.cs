using Microsoft.AspNetCore.Mvc;

namespace Routing_in_ASP.NET.Controllers
{
    // This controller is intentionally not decorated with [ApiController].
    // That allows ASP.NET Core to use conventional routing for these actions.
    public class ConventionalRoutingController : Controller
    {
        #region Conventional Routing Examples
        // No route attribute is used here.
        // So ASP.NET Core matches this action using the route pattern
        // configured in Program.cs with app.MapControllerRoute(...).
        //
        // URL:
        // GET /ConventionalRouting/Index
        public string Index()
        {
            return "Response from Conventional Routing";
        }

        // This also uses conventional routing with an optional id parameter.
        //
        // URL:
        // GET /ConventionalRouting/Details/10
        public string Details(int id)
        {
            return $"Response from Conventional Routing. Id = {id}";
        }
        #endregion
    }
}
