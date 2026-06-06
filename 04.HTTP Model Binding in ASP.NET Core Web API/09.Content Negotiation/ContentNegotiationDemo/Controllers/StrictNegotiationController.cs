using ContentNegotiationDemo.Data;
using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiationDemo.Controllers
{
    [ApiController]
    [Route("api/strict-negotiation")]
    public class StrictNegotiationController : ControllerBase
    {
        #region STRICT NEGOTIATION AND 406
        // Because Program.cs enables:
        // options.ReturnHttpNotAcceptable = true;
        //
        // the API will return 406 Not Acceptable when the client requests
        // an unsupported response format.
        //
        // Example unsupported Accept headers:
        // 1. text/csv
        // 2. application/yaml
        // 3. application/pdf
        //
        // The custom middleware then converts that 406 response into
        // a clearer JSON error message.
        #endregion

        [HttpGet("employee")]
        public IActionResult GetEmployeeForStrictModeDemo()
        {
            return Ok(EmployeeRepository.GetById(2));
        }
    }
}
