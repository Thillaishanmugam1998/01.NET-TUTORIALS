using ContentNegotiationDemo.Data;
using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiationDemo.Controllers
{
    [ApiController]
    [Route("api/basics")]
    public class BasicsController : ControllerBase
    {
        #region WHAT IS CONTENT NEGOTIATION
        // Content Negotiation is the process where the client and server
        // agree on the response format.
        //
        // Client says:
        // "I can accept JSON" or "I can accept XML"
        //
        // Server checks:
        // "Which formatter is available in my app?"
        //
        // Then ASP.NET Core serializes the same .NET object into
        // the best matching response format.
        #endregion

        #region KEY HEADERS
        // Accept:
        // Sent by the client to say what response format it wants.
        //
        // Content-Type:
        // Sent by the client for request bodies and by the server for responses
        // to say what media type is being used.
        #endregion

        [HttpGet("employee")]
        public IActionResult GetSingleEmployee()
        {
            var employee = EmployeeRepository.GetById(1);
            return Ok(employee);
        }

        [HttpGet("employees")]
        public IActionResult GetAllEmployees()
        {
            var employees = EmployeeRepository.GetAll();
            return Ok(employees);
        }
    }
}
