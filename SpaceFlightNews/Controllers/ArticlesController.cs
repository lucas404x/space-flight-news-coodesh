using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpaceFlightNews.Data.Entities;
using SpaceFlightNews.Data.Models;
using SpaceFlightNews.Services;

namespace SpaceFlightNews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : Controller 
    {
        private readonly IArticleServices _articleServices;
        public ArticlesController(IArticleServices articleServices) 
        {
            _articleServices = articleServices;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Article>>> GetAsync(int? limit, int? offset) 
        {
            Stopwatch _stopwatch = Stopwatch.StartNew();

            return new() 
            {
                Result = await _articleServices.GetArticles(limit ?? 10, offset ?? 0),
                Status = new() 
                {
                    Code = 200,
                    Message = "Articles retrieved with successful",
                },
                ElapsedTimeInMilliseconds = _stopwatch.ElapsedMilliseconds
            };
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<Article>> GetArticleAsync(string id) 
        {
            Stopwatch _stopwatch = Stopwatch.StartNew();
            return new() 
            {
                Result = await _articleServices.GetArticle(id),
                Status = new() 
                {
                    Code = 200,
                    Message = "Article found with successful"
                },
                ElapsedTimeInMilliseconds = _stopwatch.ElapsedMilliseconds 
            };
        }
    }
}