using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApiTests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task PostHtmlContentToDb_WhenValidHtmlPassed_ReturnsStatusCode200()
        {
            // Arrange
            var client = _factory.CreateClient();
            var validHtml = "<html><head><title>Test</title></head><body>Test body</body></html>";
            var contentJson = JsonConvert.SerializeObject(new { content = validHtml });

            var content = new StringContent(contentJson, Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/api/Blog/PostHtmlContentToSql", content);

            // Assert
            response.EnsureSuccessStatusCode(); // this checks that the status code is 200-299
        }

    }

}
