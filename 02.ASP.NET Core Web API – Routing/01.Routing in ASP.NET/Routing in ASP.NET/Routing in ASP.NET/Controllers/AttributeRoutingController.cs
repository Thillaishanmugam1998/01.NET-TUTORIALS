using Microsoft.AspNetCore.Mvc;

namespace Routing_in_ASP.NET.Controllers
{
    [ApiController]
    public class AttributeRoutingController : ControllerBase
    {
        #region Attribute Routing Examples
        // Here the route is written directly on the action method.
        // So this action does not depend on the conventional route pattern.
        //
        // URL:
        // GET /api/routing-demo/attribute
        [HttpGet("api/routing-demo/attribute")]
        public string Attribute()
        {
            return "Response from Attribute Routing";
        }

        // Attribute route with route parameter.
        //
        // URL:
        // GET /api/routing-demo/attribute/10
        [HttpGet("api/routing-demo/attribute/{id}")]
        public string AttributeById(int id)
        {
            return $"Response from Attribute Routing. Id = {id}";
        }
        #endregion
    }
}
