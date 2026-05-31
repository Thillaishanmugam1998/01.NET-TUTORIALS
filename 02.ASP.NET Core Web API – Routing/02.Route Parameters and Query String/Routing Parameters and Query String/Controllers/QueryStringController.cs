using Microsoft.AspNetCore.Mvc;
using Routing_Parameters_and_Query_String.Models;

namespace Routing_Parameters_and_Query_String.Controllers;

[ApiController]
[Route("api/query-string")]
public class QueryStringController : ControllerBase
{
    #region One Query String Parameter
    // Query string means the value comes after ? in the URL.
    // Example:
    // GET /api/query-string/student?id=10
    [HttpGet("student")]
    public string GetStudentById([FromQuery] int id)
    {
        return $"Query String Example - Single Value. Student Id = {id}";
    }
    #endregion

    #region Multiple Query String Parameters
    // Here multiple values come after ? and are separated by &.
    // Example:
    // GET /api/query-string/employee?employeeId=101&departmentName=hr
    [HttpGet("employee")]
    public string GetEmployeeByDepartment([FromQuery] int employeeId, [FromQuery] string departmentName)
    {
        return $"Query String Example - Multiple Values. Employee Id = {employeeId}, Department = {departmentName}";
    }
    #endregion

    #region Complex Query String Parameter
    // A complex query string means multiple query values are grouped
    // into one model object using [FromQuery].
    // Example:
    // GET /api/query-string/product?productId=200&category=electronics&brand=sony
    [HttpGet("product")]
    public string GetProductDetails([FromQuery] ProductQueryParameters request)
    {
        return $"Query String Example - Complex Model. Product Id = {request.ProductId}, Category = {request.Category}, Brand = {request.Brand}";
    }
    #endregion
}
