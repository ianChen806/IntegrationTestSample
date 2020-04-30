using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTestSample.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
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
            var httpClient = CreateHttpClient(collection =>
            {
                collection.AddScoped(r =>
                {
                    var testService = Substitute.For<ITestService>();
                    testService.WeatherForecasts().Returns(new List<WeatherForecast>
                    {
                        new WeatherForecast(),
                    });
                    return testService;
                });
            });

            var message = await httpClient.GetAsync("/WeatherForecast/Get");
            var list = await message.Content.ReadAsAsync<List<WeatherForecast>>();

            message.StatusCode.Should().Be(HttpStatusCode.OK);
            list.Count.Should().Be(1);
        }

        [Fact]
        public async Task TestDb()
        {
            var httpClient = CreateHttpClient(null);

            var message = await httpClient.GetAsync("/WeatherForecast/GetDb");

            message.StatusCode.Should().Be(HttpStatusCode.OK);
            DbOperator(context =>
            {
                var member = context.Member.FirstOrDefault(r => r.Name == "Controller");
                member.Should().NotBeNull();
            });
        }
    }
}