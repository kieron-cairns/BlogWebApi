using BlogWebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlogWebApiTests
{
    public class ControllerTests
    {
        [Fact]
        public async Task PostHtmlContentToDb_WhenValidHtmlPassed_ReturnsStatusCode200()
        {
            // Arrange
            var controller = new BlogController();
            var validHtml = "<html><head><title>Test</title></head><body>Test body</body></html>";

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(validHtml));
            controller.ControllerContext.HttpContext = httpContext;

            // Act
            var result = await controller.PostHtmlContentToDb();

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task PostHtmlContentToDb_WhenInvalidHtmlPassed_ReturnsStatusCode400()
        {
            // Arrange
            var controller = new BlogController();
            var invalidHtml = "<html><body><p>Test</p></body></html"; // Missing closing angle bracket

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(invalidHtml));
            controller.ControllerContext.HttpContext = httpContext;

            // Act
            var result = await controller.PostHtmlContentToDb();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, objectResult.StatusCode);
        }

    }
}