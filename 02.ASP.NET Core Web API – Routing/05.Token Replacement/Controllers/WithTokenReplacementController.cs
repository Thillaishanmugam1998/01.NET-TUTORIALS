using Microsoft.AspNetCore.Mvc;

namespace Token_Replacement.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WithTokenReplacementController : ControllerBase
    {
        #region What Is Token Replacement
        // Token replacement means ASP.NET Core replaces special tokens
        // with actual values at runtime.
        //
        // Example: 
        // [Route("api/[controller]/[action]")]
        //
        // If the controller name is WithTokenReplacementController
        // and the action name is GetTokenInfo,
        // then the final route becomes:
        // /api/WithTokenReplacement/GetTokenInfo
        #endregion

        #region Types of Tokens
        // Common built-in route tokens are:
        //
        // [controller]
        // Replaced with the controller name without the Controller suffix.
        //
        // [action]
        // Replaced with the action method name.
        //
        // [area]
        // Replaced with the current area name when areas are used.
        #endregion

        #region Token Replacement Example
        // URL:
        // GET /api/WithTokenReplacement/GetTokenInfo
        [HttpGet]
        //[Route("[action]")]
        public string GetTokenInfo()
        {
            return "Response from controller with token replacement";
        }
        #endregion

        #region Multiple URLs for Same Resource
        // The controller and action parts use token replacement.
        // In addition, we can still add multiple custom route templates
        // on the same action.
        //
        // URLs:
        // GET /api/WithTokenReplacement/GetStudentResource
        // GET /api/WithTokenReplacement/student-details
        // GET /api/WithTokenReplacement/student-info
        [HttpGet]
        [HttpGet("~/api/WithTokenReplacement/student-details")]
        [HttpGet("~/api/WithTokenReplacement/student-info")]
        public string GetStudentResource()
        {
            return "Response from controller with token replacement - multiple URLs for same resource";
        }
        #endregion
    }
}
