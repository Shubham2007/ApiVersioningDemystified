using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace ApiVersioningDemo.Controllers
{
    [ApiController]
    //[Route("v{version:apiVersion}/[controller]")]
    [Route("[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("1.1")]
    [ApiVersion("2.0")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        [MapToApiVersion("1.0")]
        public object Get(ApiVersion apiVersion) // ApiVersion parameter works in .net core 3.x + versions
        {
            var rng = new Random();
            return new
            {
                WeatherData = Enumerable.Range(1, 1).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
            .ToArray(),
                //ApiVersion = HttpContext.GetRequestedApiVersion()
                ApiVersion = apiVersion
            };
        }

        [HttpGet("")]
        [MapToApiVersion("2.0")]
        public object GetV2_0()
        {
            var rng = new Random();
            return new
            {
                WeatherData = Enumerable.Range(1, 2).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
            .ToArray(),
                ApiVersion = HttpContext.GetRequestedApiVersion()
            };
        }

        [HttpGet("")]
        [MapToApiVersion("3.0")]
        public object GetV3_0()
        {
            var rng = new Random();
            return new
            {
                WeatherData = Enumerable.Range(1, 3).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
            .ToArray(),
                ApiVersion = HttpContext.GetRequestedApiVersion()
            };
        }
    }
}
