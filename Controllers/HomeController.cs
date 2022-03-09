using Microsoft.AspNetCore.Mvc;

namespace space_flight_news_coodesh.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : Controller
{
    public HomeController() {}

    [HttpGet]
    public OkObjectResult Get() 
    {
        return Ok("._.");
    }
}
