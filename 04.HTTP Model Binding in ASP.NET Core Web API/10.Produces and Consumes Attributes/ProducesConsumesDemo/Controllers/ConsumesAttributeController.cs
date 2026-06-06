using ProducesConsumesDemo.Data;
using ProducesConsumesDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProducesConsumesDemo.Controllers
{
    [ApiController]
    [Route("api/consumes")]
    public class ConsumesAttributeController : ControllerBase
    {
        #region WHAT [CONSUMES] DOES
        // [Consumes] tells ASP.NET Core what request body format
        // the action is willing to accept.
        //
        // In simple words:
        // It defines the media type of the INPUT.
        //
        // Common examples:
        // 1. application/json
        // 2. application/xml
        // 3. multipart/form-data
        //
        // Important note:
        // [Consumes] is about INPUT.
        // It does not decide the response format by itself.
        #endregion

        #region SAMPLE 1 - ACCEPT JSON ONLY
        // This endpoint accepts only:
        // Content-Type: application/json
        //
        // If the client sends XML to this endpoint,
        // ASP.NET Core returns 415 Unsupported Media Type.
        #endregion
        [HttpPost("json-only")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public IActionResult CreateProductFromJson([FromBody] ProductCreateRequest request)
        {
            var product = ProductRepository.Add(request);
            return Ok(product);
        }

        #region SAMPLE 2 - ACCEPT XML ONLY
        // This endpoint accepts only:
        // Content-Type: application/xml
        //
        // If the client sends JSON here,
        // ASP.NET Core returns 415 Unsupported Media Type.
        #endregion
        [HttpPost("xml-only")]
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public IActionResult CreateProductFromXml([FromBody] ProductCreateRequest request)
        {
            var product = ProductRepository.Add(request);
            return Ok(product);
        }
    }
}
