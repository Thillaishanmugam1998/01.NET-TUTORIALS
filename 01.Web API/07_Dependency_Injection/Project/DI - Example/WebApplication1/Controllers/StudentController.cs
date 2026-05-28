using Microsoft.AspNetCore.Mvc;
using WebApplication1.Implementation;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {

        public StudentService1 StudentService;

        //Without dependency injection, we can create an instance of the service directly in the controller. However, this approach is not recommended as it tightly couples the controller to the service implementation and makes it difficult to test and maintain the code.
        public StudentController()
        {
            StudentService = new StudentService1();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var student = StudentService.GetStudents();
            return View(student);
        }
    }
}
