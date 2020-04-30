using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
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
                builder.UseEnvironment("Test");
                if (configureServices != null)
                {
                    builder.ConfigureTestServices(configureServices);
                }

                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<MyDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("Test");
                    });

                    var serviceProvider = services.BuildServiceProvider();
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var db = scope.ServiceProvider.GetRequiredService<MyDbContext>();
                        db.Database.EnsureDeleted();
                        db.Database.EnsureCreated();
                        InitDb(db);
                    }
                });
            });
            return _webHost.CreateClient();
        }

        public void DbOperator(Action<MyDbContext> action)
        {
            using (var serviceScope = _webHost.Services.CreateScope())
            {
                var myDbContext = serviceScope.ServiceProvider.GetRequiredService<MyDbContext>();
                action(myDbContext);
            }
        }

        private void InitDb(MyDbContext db)
        {
            // do something.

            db.Member.Add(new Member()
            {
                Name = "Test",
            });
            db.SaveChanges();
        }
    }
}