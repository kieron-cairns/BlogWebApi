using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
       [HttpPost("PostHtmlContentToSql")]
       [ProducesResponseType(StatusCodes.Status200OK)]
       [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostHtmlContentToDb()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string htmlContent = await reader.ReadToEndAsync();
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(htmlContent);
                    return StatusCode(200);
                }
                catch (XmlException)
                {
                    return StatusCode(400, "Invalid HTML");
                }
            }
        }
    }
}
