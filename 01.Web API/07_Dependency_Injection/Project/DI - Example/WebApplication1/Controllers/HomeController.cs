using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {

        public IStudentService StudentService;

        public HomeController(IStudentService _studentService)
        {
            StudentService = _studentService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return Ok(StudentService.GetStudents());
        }
    }
}
