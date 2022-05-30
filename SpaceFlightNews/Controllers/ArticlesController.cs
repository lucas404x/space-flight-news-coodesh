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
        public async Task<IActionResult> GetAsync(int? limit, int? offset)
        {
            Stopwatch _stopwatch = Stopwatch.StartNew();

            return Ok(new ApiResponse<List<Article>>()
            {
                Result = await _articleServices.GetArticles(limit ?? 10, offset ?? 0),
                ElapsedTimeInMilliseconds = _stopwatch.ElapsedMilliseconds
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticleAsync(string id)
        {
            Stopwatch _stopwatch = Stopwatch.StartNew();
            return Ok(new ApiResponse<Article>()
            {
                Result = await _articleServices.GetArticle(id),
                ElapsedTimeInMilliseconds = _stopwatch.ElapsedMilliseconds
            });
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserArticle article)
        {
            Stopwatch _stopwatch = Stopwatch.StartNew();
            return Ok(new ApiResponse<bool>()
            {
                Result = await _articleServices.AddArticle(article),
                ElapsedTimeInMilliseconds = _stopwatch.ElapsedMilliseconds
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] UserArticle updatedArticle)
        {
            Stopwatch _stopwatch = Stopwatch.StartNew();
            return Ok(new ApiResponse<bool>()
            {
                Result = await _articleServices.UpdateArticle(id, updatedArticle),
                ElapsedTimeInMilliseconds = _stopwatch.ElapsedMilliseconds
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id) 
        {
            Stopwatch _stopwatch = Stopwatch.StartNew();
            return Ok(new ApiResponse<bool>()
            {
                Result = await _articleServices.DeleteArticle(id),
                ElapsedTimeInMilliseconds = _stopwatch.ElapsedMilliseconds
            });
        }
    }
}