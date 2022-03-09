using Microsoft.AspNetCore.Mvc;
using SpaceFlightNews.Data.Models;

namespace SpaceFlightNews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : Controller 
    {
        public ArticlesController() {}

        [HttpGet]
        public ApiResponse<dynamic> Get() 
        {
            return new() 
            {
                Result = "OK"
            };
        }
    }
}