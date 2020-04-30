using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTestSample.Test
{
    public class UnitTest1 : TestBase
    {
        public UnitTest1(WebApplicationFactory<Startup> applicationFactory)
            : base(applicationFactory)
        {
        }

        [Fact]
        public async Task Test1()
        {
            var httpClient = _applicationFactory.CreateClient();

            var message = await httpClient.GetAsync("/WeatherForecast");
            var list = await message.Content.ReadAsAsync<List<WeatherForecast>>();

            Assert.Equal(5, list.Count);
        }
    }
}