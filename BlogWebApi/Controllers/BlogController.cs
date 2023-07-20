using Microsoft.AspNetCore.Mvc;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
       [HttpPost("PostHtmlContentToSql")]
       public IActionResult PostHtmlContentToDb(string htmlContent)
       {

            return StatusCode(200);
       }
    }
}
