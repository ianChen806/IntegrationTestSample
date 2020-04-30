using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTestSample.Test
{
    public class TestBase
        : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly WebApplicationFactory<Startup> _applicationFactory;

        public TestBase(WebApplicationFactory<Startup> applicationFactory)
        {
            _applicationFactory = applicationFactory;
        }
    }
}