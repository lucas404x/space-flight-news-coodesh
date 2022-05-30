using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpaceFlightNews.Data.Models;

namespace SpaceFlightNews.Controllers;

[ApiController]
[Route("")]
public class HomeController : Controller
{
    public HomeController() {}

    [HttpGet]
    public IActionResult Get() 
    {
        Stopwatch _stopwatch = Stopwatch.StartNew();

        return Ok(new ApiResponse<string>() 
        {
            Result = "Back-end Challenge 2021 🏅 - Space Flight News",
            ElapsedTimeInMilliseconds = _stopwatch.ElapsedMilliseconds
        });
    }
}
