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
    public ApiResponse<string> Get() 
    {
        Stopwatch _stopwatch = Stopwatch.StartNew();

        return new() 
        {
            Result = "Back-end Challenge 2021 🏅 - Space Flight News",
            Status = new() 
            {
                Code = 200
            },
            ElapsedTimeInMilliseconds = _stopwatch.ElapsedMilliseconds
        };
    }
}
