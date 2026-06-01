using Microsoft.AspNetCore.Mvc;

namespace Route_Prefix_in_ASP.NET.Controllers;

[ApiController]
[Route("api/with-route-prefix/student")]
public class WithRoutePrefixController : ControllerBase
{
    #region What Is Route Prefix
    // Route prefix means we define a common route part at controller level.
    // Then each action only adds the remaining route segment.
    //
    // Example:
    // [Route("api/with-route-prefix/student")]
    //
    // This prefix is shared by all actions in this controller.
    // So repeated route text is reduced.
    #endregion

    #region Why Use Route Prefix
    // Route prefix helps to:
    // 1. avoid repeating the same common route part
    // 2. keep routes cleaner and easier to maintain
    // 3. group related actions under one common URL path
    #endregion

    #region All Students Example
    // Final URL:
    // GET /api/with-route-prefix/student/all
    [HttpGet("all")]
    public string GetAllStudents()
    {
        return "Response from controller with route prefix - all students";
    }
    #endregion

    #region Student By Id Example
    // Final URL:
    // GET /api/with-route-prefix/student/by-id/10
    [HttpGet("by-id/{id}")]
    public string GetStudentById(int id)
    {
        return $"Response from controller with route prefix - student id = {id}";
    }
    #endregion
}
