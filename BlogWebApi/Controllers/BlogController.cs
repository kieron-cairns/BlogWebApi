using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
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
                string jsonString = await reader.ReadToEndAsync();
                try
                {
                    JsonDocument doc = JsonDocument.Parse(jsonString);
                    string htmlContent = doc.RootElement.GetProperty("content").GetString();

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(htmlContent);
                    return StatusCode(200);
                }
                catch (JsonException)
                {
                    return StatusCode(400, "Invalid JSON");
                }
                catch (XmlException)
                {
                    return StatusCode(400, "Invalid HTML");
                }
            }
        }
    }
}
