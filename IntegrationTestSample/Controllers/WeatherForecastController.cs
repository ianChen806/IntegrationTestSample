using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IntegrationTestSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly TestService _testService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, TestService testService)
        {
            _logger = logger;
            _testService = testService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _testService.WeatherForecasts();
        }
    }
}