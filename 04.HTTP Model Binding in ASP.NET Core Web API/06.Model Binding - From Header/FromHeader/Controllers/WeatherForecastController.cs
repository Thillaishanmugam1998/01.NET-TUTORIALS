using Microsoft.AspNetCore.Mvc;

namespace FromHeader.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        [HttpGet("FromHeader")]
        public IActionResult Get([FromHeader] string Authkey)
        {

            if(Authkey != "12345")
            {
                return Unauthorized("Not Valid");
            }
            else
            {
                return Ok(Authkey);
            }
               
        }
            
    }
}
