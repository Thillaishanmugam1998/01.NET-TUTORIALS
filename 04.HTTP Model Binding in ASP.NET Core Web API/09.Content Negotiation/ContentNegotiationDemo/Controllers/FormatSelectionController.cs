using ContentNegotiationDemo.Data;
using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiationDemo.Controllers
{
    [ApiController]
    [Route("api/format-selection")]
    public class FormatSelectionController : ControllerBase
    {
        #region OUTPUT FORMATTER BEHAVIOR
        // This controller is used to test how ASP.NET Core chooses
        // an output formatter based on the Accept header.
        //
        // Try these requests:
        // 1. Accept: application/json
        // 2. Accept: application/xml
        // 3. Accept: application/xml,application/json
        // 4. No Accept header
        //
        // Expected with current Program.cs configuration:
        // 1. JSON when JSON is explicitly requested
        // 2. XML when XML is explicitly requested
        // 3. The first supported media type wins
        // 4. JSON is used as the default response
        #endregion

        [HttpGet("employee/{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = EmployeeRepository.GetById(id);

            if (employee is null)
            {
                return NotFound($"No employee found with id {id}.");
            }

            return Ok(employee);
        }

        [HttpGet("employees")]
        public IActionResult GetEmployees()
        {
            return Ok(EmployeeRepository.GetAll());
        }
    }
}
