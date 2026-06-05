using FromRoute.Models;
using Microsoft.AspNetCore.Mvc;

namespace FromRoute.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        [HttpGet]
        [Route("Index/{message}")]
        public IActionResult Index([FromRoute] string message)
        {
            return Ok(message);
        }

        [HttpGet("GetName/{Name}/{Age}")]
        public IActionResult GetName([FromRoute] Students student)
        {
            return Ok(student);
        }


        [HttpGet("{age}")]
        public IActionResult Get([FromRoute] string age)
        {
            return Ok(age);
        }

    }
}
