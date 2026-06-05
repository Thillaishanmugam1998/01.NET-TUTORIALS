using Microsoft.AspNetCore.Mvc;

namespace FromBody.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult Index(User user)
        {
            return Ok(user);
        }

    }


    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
