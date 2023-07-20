using BlogWebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BlogWebApiTests
{
    public class ControllerTests
    {
        [Fact]
        public void PostHtmlContentToDb_WhenValidHtmlPassed_ReturnsStatusCode200()
        {
            //Arrange 
            var controller = new BlogController();
            string validHtml = "<html><head><title>Test</title></head><body>Test body</body></html>";

            //Act
            var result = controller.PostHtmlContentToDb(validHtml);

            //Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
    }
}