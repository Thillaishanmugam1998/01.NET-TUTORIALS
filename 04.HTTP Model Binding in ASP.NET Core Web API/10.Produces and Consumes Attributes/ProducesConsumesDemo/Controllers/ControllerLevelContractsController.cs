using ProducesConsumesDemo.Data;
using ProducesConsumesDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ProducesConsumesDemo.Controllers
{
    [ApiController]
    [Route("api/contracts")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ControllerLevelContractsController : ControllerBase
    {
        #region CONTROLLER LEVEL [PRODUCES] AND [CONSUMES]
        // These attributes are applied at the controller level:
        // [Produces("application/json")]
        // [Consumes("application/json")]
        //
        // That means every action inside this controller
        // will use JSON as the default contract unless an action
        // explicitly overrides that behavior.
        //
        // So by default:
        // 1. Responses are JSON
        // 2. Request bodies must be JSON
        #endregion

        #region SAMPLE 1 - DEFAULT CONTROLLER LEVEL JSON CONTRACT
        // Input expected:
        // Content-Type: application/json
        //
        // Output produced:
        // application/json
        #endregion
        [HttpPost("products")]
        public IActionResult AddProduct(ProductCreateRequest request)
        {
            var product = ProductRepository.Add(request);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = product.Id },
                product);
        }

        #region SAMPLE 2 - DEFAULT JSON RESPONSE
        // Because the controller has [Produces("application/json")],
        // this action returns JSON by default.
        #endregion
        [HttpGet("products/{id:int}")]
        public IActionResult GetProductById(int id)
        {
            var product = ProductRepository.GetById(id);

            if (product is null)
            {
                return NotFound($"No product found with id {id}.");
            }

            return Ok(product);
        }

        #region SAMPLE 3 - OVERRIDE OUTPUT TO XML
        // This action overrides only [Produces].
        //
        // Result:
        // Response will be XML for this action.
        // The rest of the controller still uses JSON defaults.
        #endregion
        [HttpGet("products/{id:int}/xml")]
        [Produces("application/xml")]
        public IActionResult GetProductByIdXml(int id)
        {
            var product = ProductRepository.GetById(id);

            if (product is null)
            {
                return NotFound($"No product found with id {id}.");
            }

            return Ok(product);
        }

        #region SAMPLE 4 - OVERRIDE INPUT AND OUTPUT TO XML
        // This action accepts XML request body and produces XML response.
        //
        // It overrides the controller-level JSON contract only for this endpoint.
        #endregion
        [HttpPost("products/xml")]
        [Consumes("application/xml")]
        [Produces("application/xml")]
        public IActionResult AddProductXml(ProductCreateRequest request)
        {
            var product = ProductRepository.Add(request);
            return Ok(product);
        }

        #region SAMPLE 5 - OVERRIDE INPUT TO MULTIPART FORM DATA
        // This action accepts file uploads using:
        // Content-Type: multipart/form-data
        //
        // Response still remains JSON because [Produces("application/json")]
        // is inherited from the controller level.
        #endregion
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            return Ok(new
            {
                FileName = file.FileName,
                Size = file.Length,
                Message = "File uploaded successfully."
            });
        }
    }
}
