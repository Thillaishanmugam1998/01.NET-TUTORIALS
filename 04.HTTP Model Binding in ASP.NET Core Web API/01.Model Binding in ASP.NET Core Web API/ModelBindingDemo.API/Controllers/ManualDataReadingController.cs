using System.Text.Json;
using ModelBindingDemo.API.Data;
using ModelBindingDemo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ModelBindingDemo.API.Controllers;

[Route("api/manual-data-reading")]
public class ManualDataReadingController : ControllerBase
{
    #region WHAT IS MODEL BINDING?
    // Model Binding is the ASP.NET Core feature that takes incoming request data
    // and automatically maps it into action parameters or model objects.
    //
    // In this controller we are NOT using model binding on purpose.
    // We read Request.Body, Request.Query, Request.RouteValues, and Request.Headers manually
    // so learners can clearly see the extra work involved.
    #endregion

    #region WHY DO WE NEED MODEL BINDING?
    // Without model binding, every action must manually:
    // 1. Read data from the HTTP request
    // 2. Convert strings/JSON into .NET types
    // 3. Handle missing values and validation checks
    //
    // That leads to repetitive code, more chances for bugs, and harder maintenance.
    #endregion

    #region TYPES OF MODEL BINDING
    // Common binding sources in ASP.NET Core Web API:
    // [FromBody]  -> request body JSON
    // [FromRoute] -> route values like /students/5
    // [FromQuery] -> query string values like ?department=CSE
    // [FromHeader] -> header values like X-Trace-Id
    //
    // This controller shows the manual equivalent of reading those values.
    #endregion

    #region ADVANTAGES AND DISADVANTAGES OF MANUAL DATA READING
    // Advantages:
    // 1. Useful for learning how raw HTTP request data is structured
    // 2. Gives full low-level control over the request
    //
    // Disadvantages:
    // 1. More boilerplate code
    // 2. Repeated parsing and validation logic
    // 3. Easier to make mistakes
    // 4. Harder to test and maintain
    //
    // Recommendation:
    // Manual reading is good only for understanding the basics or for rare custom scenarios.
    // For normal Web API development, model binding is the recommended approach.
    #endregion

    [HttpPost("body")]
    public async Task<IActionResult> CreateStudentByReadingBodyManually()
    {
        using var reader = new StreamReader(Request.Body);
        var rawBody = await reader.ReadToEndAsync();

        if (string.IsNullOrWhiteSpace(rawBody))
        {
            return BadRequest("Request body is empty.");
        }

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var request = JsonSerializer.Deserialize<StudentCreateRequest>(rawBody, options);

        if (request is null)
        {
            return BadRequest("Unable to deserialize request body.");
        }

        var createdStudent = StudentRepository.Add(request);

        return Ok(new
        {
            Approach = "Manual body reading",
            RawBody = rawBody,
            CreatedStudent = createdStudent
        });
    }

    [HttpGet("route/{id}")]
    public IActionResult GetStudentByReadingRouteManually()
    {
        var rawId = Request.RouteValues["id"]?.ToString();

        if (!int.TryParse(rawId, out var id))
        {
            return BadRequest("Route id is missing or invalid.");
        }

        var student = StudentRepository.GetById(id);

        if (student is null)
        {
            return NotFound($"No student found with id {id}.");
        }

        return Ok(new
        {
            Approach = "Manual route reading",
            RawRouteValue = rawId,
            Student = student
        });
    }

    [HttpGet("query")]
    public IActionResult SearchStudentsByReadingQueryManually()
    {
        var department = Request.Query["department"].ToString();
        var rawMinimumAge = Request.Query["minimumAge"].ToString();

        int? minimumAge = null;
        if (!string.IsNullOrWhiteSpace(rawMinimumAge))
        {
            if (!int.TryParse(rawMinimumAge, out var parsedAge))
            {
                return BadRequest("minimumAge must be a valid number.");
            }

            minimumAge = parsedAge;
        }

        var students = StudentRepository.Search(department, minimumAge);

        return Ok(new
        {
            Approach = "Manual query reading",
            Department = department,
            RawMinimumAge = rawMinimumAge,
            Students = students
        });
    }

    [HttpGet("header")]
    public IActionResult ReadHeaderManually()
    {
        var traceId = Request.Headers["X-Trace-Id"].ToString();

        if (string.IsNullOrWhiteSpace(traceId))
        {
            return BadRequest("X-Trace-Id header is required.");
        }

        return Ok(new
        {
            Approach = "Manual header reading",
            TraceId = traceId,
            Message = "Header value read directly from Request.Headers."
        });
    }
}
