using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationTestSample.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        private readonly ITestService _testService;

        public WeatherForecastController(ITestService testService, MyDbContext dbContext)
        {
            _testService = testService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _testService.WeatherForecasts();
        }

        [HttpGet]
        public void GetDb()
        {
            _dbContext.Member.Add(new Member
            {
                Name = "Controller"
            });
            _dbContext.SaveChanges();
        }
    }
}