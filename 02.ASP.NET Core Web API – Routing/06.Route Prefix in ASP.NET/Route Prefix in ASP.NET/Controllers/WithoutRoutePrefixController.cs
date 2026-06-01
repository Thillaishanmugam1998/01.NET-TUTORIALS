using Microsoft.AspNetCore.Mvc;

namespace Route_Prefix_in_ASP.NET.Controllers;

[ApiController]
public class WithoutRoutePrefixController : ControllerBase
{
    #region What Is Without Route Prefix
    // Without route prefix, each action writes the full route manually.
    // So the common route part is repeated again and again in every method.
    //
    // Example:
    // [HttpGet("api/without-route-prefix/student/all")]
    // [HttpGet("api/without-route-prefix/student/by-id/{id}")]
    //
    // This works, but repeated route text makes maintenance harder.
    #endregion

    #region All Students Example
    // URL:
    // GET /api/without-route-prefix/student/all
    [HttpGet("api/without-route-prefix/student/all")]
    public string GetAllStudents()
    {
        return "Response from controller without route prefix - all students";
    }
    #endregion

    #region Student By Id Example
    // URL:
    // GET /api/without-route-prefix/student/by-id/10
    [HttpGet("api/without-route-prefix/student/by-id/{id}")]
    public string GetStudentById(int id)
    {
        return $"Response from controller without route prefix - student id = {id}";
    }
    #endregion
}
