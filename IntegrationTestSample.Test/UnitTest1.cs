using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTestSample.Test
{
    public class UnitTest1 : TestBase
    {
        public UnitTest1(WebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Test1()
        {
            var httpClient = CreateHttpClient(null);

            var message = await httpClient.GetAsync("/WeatherForecast");
            var list = await message.Content.ReadAsAsync<List<WeatherForecast>>();

            message.StatusCode.Should().Be(HttpStatusCode.OK);
            list.Count.Should().Be(5);
        }
    }
}