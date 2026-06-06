using ProducesConsumesDemo.Data;
using Microsoft.AspNetCore.Mvc;

namespace ProducesConsumesDemo.Controllers
{
    [ApiController]
    [Route("api/produces")]
    public class ProducesAttributeController : ControllerBase
    {
        #region WHAT [PRODUCES] DOES
        // [Produces] tells ASP.NET Core what response format
        // an action or controller should return.
        //
        // In simple words:
        // It defines the media type of the response.
        //
        // Common examples:
        // 1. application/json
        // 2. application/xml
        //
        // Important note:
        // [Produces] is about OUTPUT.
        // It does not control what input the action accepts.
        #endregion

        #region SAMPLE 1 - FORCE JSON OUTPUT
        // Even if the client asks for XML, this action says:
        // "I will produce JSON."
        #endregion
        [HttpGet("json/{id:int}")]
        [Produces("application/json")]
        public IActionResult GetProductAsJson(int id)
        {
            var product = ProductRepository.GetById(id);

            if (product is null)
            {
                return NotFound($"No product found with id {id}.");
            }

            return Ok(product);
        }

        #region SAMPLE 2 - FORCE XML OUTPUT
        // This action always produces XML output.
        // The same Product object is serialized as XML.
        #endregion
        [HttpGet("xml/{id:int}")]
        [Produces("application/xml")]
        public IActionResult GetProductAsXml(int id)
        {
            var product = ProductRepository.GetById(id);

            if (product is null)
            {
                return NotFound($"No product found with id {id}.");
            }

            return Ok(product);
        }
    }
}
