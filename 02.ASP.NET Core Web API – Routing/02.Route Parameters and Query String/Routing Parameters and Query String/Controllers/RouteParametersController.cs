using Microsoft.AspNetCore.Mvc;
using Routing_Parameters_and_Query_String.Models;

namespace Routing_Parameters_and_Query_String.Controllers;

[ApiController]
[Route("api/route-parameters")]
public class RouteParametersController : ControllerBase
{
    #region One Route Parameter
    // Route parameter means the value comes from the URL path itself.
    // Example:
    // GET /api/route-parameters/student/10
    [HttpGet("student/{id}")]
    public string GetStudentById(int id)
    {
        return $"Route Parameter Example - Single Value. Student Id = {id}";
    }
    #endregion

    #region Multiple Route Parameters
    // Here multiple values come from different parts of the URL path.
    // Example:
    // GET /api/route-parameters/employee/101/department/hr
    [HttpGet("employee/{employeeId}/department/{departmentName}")]
    public string GetEmployeeByDepartment(int employeeId, string departmentName)
    {
        return $"Route Parameter Example - Multiple Values. Employee Id = {employeeId}, Department = {departmentName}";
    }
    #endregion

    #region Complex Route Parameter
    // A complex route parameter means multiple route values are grouped
    // into one model object using [FromRoute].
    // Example:
    // GET /api/route-parameters/order/5001/customer/77/city/chennai
    [HttpGet("order/{orderId}/customer/{customerId}/city/{city}")]
    public string GetOrderDetails([FromRoute] OrderRouteParameters request)
    {
        return $"Route Parameter Example - Complex Model. Order Id = {request.OrderId}, Customer Id = {request.CustomerId}, City = {request.City}";
    }
    #endregion
}
