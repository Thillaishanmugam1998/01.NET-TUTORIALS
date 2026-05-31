using Microsoft.AspNetCore.Mvc;

namespace Token_Replacement.Controllers
{
    [ApiController]
    [Route("api/without-token-replacement")]
    public class WithoutTokenReplacementController : ControllerBase
    {
        #region About Without Token Replacement
        // Without token replacement, we write the route text manually.
        // That means we explicitly type the controller name, action name,
        // or any custom route segment by ourselves.
        //
        // Example:
        // [Route("api/without-token-replacement")]
        // [HttpGet("single-url")]
        //
        // Here ASP.NET Core uses the exact text that we wrote.
        #endregion

        #region Single URL Example
        // URL:
        // GET /api/without-token-replacement/single-url
        [HttpGet("single-url")]
        public string GetSingleUrl()
        {
            return "Response from controller without token replacement - single URL";
        }
        #endregion

        #region Multiple URLs for Same Resource
        // Here one action is accessible with multiple URLs.
        // All these URLs reach the same method.
        //
        // URLs:
        // GET /api/without-token-replacement/student-details
        // GET /api/without-token-replacement/student-info
        [HttpGet("student-details")]
        [HttpGet("student-info")]
        public string GetStudentResource()
        {
            return "Response from controller without token replacement - multiple URLs for same resource";
        }
        #endregion
    }
}
