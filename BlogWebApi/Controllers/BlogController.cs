using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
       [HttpPost("PostHtmlContentToSql")]
       public IActionResult PostHtmlContentToDb(string htmlContent)
       {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(htmlContent);
                return StatusCode(200);
            }
            catch (XmlException)
            {
                return StatusCode(400, "Invalid Html");
            }
       }
    }
}
