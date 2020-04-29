using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTestSample.Test
{
    public class TestBase
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _applicationFactory;

        public TestBase(WebApplicationFactory<Startup> applicationFactory)
        {
            _applicationFactory = applicationFactory;
        }
    }
}