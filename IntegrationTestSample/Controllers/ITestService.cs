using System.Collections.Generic;

namespace IntegrationTestSample.Controllers
{
    public interface ITestService
    {
        IEnumerable<WeatherForecast> WeatherForecasts();
    }
}