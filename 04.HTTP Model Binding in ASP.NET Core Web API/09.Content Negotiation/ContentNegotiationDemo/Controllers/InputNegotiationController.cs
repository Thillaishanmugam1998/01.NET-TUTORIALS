using ContentNegotiationDemo.Data;
using ContentNegotiationDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiationDemo.Controllers
{
    [ApiController]
    [Route("api/input-negotiation")]
    public class InputNegotiationController : ControllerBase
    {
        #region INPUT NEGOTIATION EXPLANATION
        // Output negotiation uses the Accept header.
        // Input negotiation uses the Content-Type header.
        //
        // Example:
        // Content-Type: application/json
        // means ASP.NET Core should use the JSON input formatter
        // to deserialize the body into the CreateEmployeeRequest object.
        //
        // If the body format is unsupported, ASP.NET Core returns 415.
        #endregion

        [HttpPost("employees")]
        public IActionResult CreateEmployee([FromBody] CreateEmployeeRequest request)
        {
            var employee = EmployeeRepository.Add(request);

            return CreatedAtAction(
                nameof(GetCreatedEmployee),
                new { id = employee.Id },
                employee);
        }

        [HttpGet("employees/{id:int}")]
        public IActionResult GetCreatedEmployee(int id)
        {
            var employee = EmployeeRepository.GetById(id);

            if (employee is null)
            {
                return NotFound($"No employee found with id {id}.");
            }

            return Ok(employee);
        }
    }
}
