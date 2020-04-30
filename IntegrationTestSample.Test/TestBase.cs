using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntegrationTestSample.Test
{
    public class TestBase
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private WebApplicationFactory<Startup> _webHost;

        public TestBase(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        protected HttpClient CreateHttpClient(Action<IServiceCollection> configureServices)
        {
            _webHost = _factory.WithWebHostBuilder(builder =>
            {
                if (configureServices != null)
                {
                    builder.ConfigureServices(configureServices);
                }
            });
            return _webHost.CreateClient();
        }
    }
}