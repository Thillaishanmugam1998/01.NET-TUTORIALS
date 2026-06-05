using Microsoft.AspNetCore.Mvc;

namespace FromQuery.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet]
        [Route("[action]")]
        public string GetFromQuery([FromQuery] string name)
        {
            return $"Hello, {name}!";
        }

        [HttpGet]
        [Route("[action]")]
        public string GetStudentInfo([FromQuery] string name, [FromQuery] string age)
        {
            return $"Hello, {name}! & Age {age}";
        }


        [HttpGet]
        [Route("[action]")]
        public string GetWeatherInfo(WeatherForecast request)
        {
            return $"{request.Date} - {request.TemperatureF} - {request.TemperatureC}";
        }
    }
}
