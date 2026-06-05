using ModelBindingDemo.API.Data;
using ModelBindingDemo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ModelBindingDemo.API.Controllers;

[ApiController]
[Route("api/model-binding")]
public class ModelBindingController : ControllerBase
{
    #region WHAT IS MODEL BINDING?
    // Model Binding automatically maps incoming request data to action parameters
    // and complex .NET objects.
    //
    // Example:
    // JSON body      -> StudentCreateRequest
    // Route segment  -> int id
    // Query string   -> StudentSearchRequest
    // HTTP header    -> string traceId
    #endregion

    #region WHY MODEL BINDING IS NEEDED
    // Model Binding removes manual parsing code and makes actions cleaner.
    // It improves readability, validation support, and consistency across the API.
    //
    // When [ApiController] is used:
    // 1. ASP.NET Core automatically validates model state
    // 2. 400 Bad Request is returned automatically for invalid models
    // 3. Binding source inference becomes smarter
    #endregion

    #region TYPES OF MODEL BINDING USED HERE
    // [FromBody]  : Reads JSON body and maps it to a model object
    // [FromRoute] : Reads values from route segments
    // [FromQuery] : Reads values from query string
    // [FromHeader]: Reads values from HTTP headers
    #endregion

    #region ADVANTAGES AND DISADVANTAGES OF MODEL BINDING
    // Advantages:
    // 1. Less code and less repetition
    // 2. Built-in validation support
    // 3. Cleaner action methods
    // 4. Easier maintenance and testing
    //
    // Disadvantages:
    // 1. Beginners may not immediately see where values come from
    // 2. Very custom parsing scenarios may still need manual handling
    //
    // Recommendation:
    // Model Binding + [ApiController] is the recommended approach for most ASP.NET Core Web APIs.
    #endregion

    [HttpPost("body")]
    public ActionResult<Student> CreateStudent([FromBody] StudentCreateRequest request)
    {
        var createdStudent = StudentRepository.Add(request);

        return CreatedAtAction(
            nameof(GetStudentById),
            new { id = createdStudent.Id },
            createdStudent);
    }

    [HttpGet("route/{id}")]
    public ActionResult<Student> GetStudentById([FromRoute] int id)
    {
        var student = StudentRepository.GetById(id);

        if (student is null)
        {
            return NotFound($"No student found with id {id}.");
        }

        return student;
    }

    [HttpGet("query")]
    public ActionResult<IEnumerable<Student>> SearchStudents([FromQuery] StudentSearchRequest request)
    {
        var students = StudentRepository.Search(request.Department, request.MinimumAge);
        return Ok(students);
    }

    [HttpGet("header")]
    public IActionResult ReadHeader([FromHeader(Name = "X-Trace-Id")] string traceId)
    {
        return Ok(new
        {
            Approach = "Model binding",
            TraceId = traceId,
            Message = "Header value was automatically bound to the action parameter."
        });
    }
}
