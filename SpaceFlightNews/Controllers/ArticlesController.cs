using Microsoft.AspNetCore.Mvc;
using SpaceFlightNews.Data.Entities;
using SpaceFlightNews.Data.Models;
using SpaceFlightNews.Infrastructure.Repositories;

namespace SpaceFlightNews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticlesController : Controller 
    {
        private readonly IArticleRepository _articleRepository;
        public ArticlesController(IArticleRepository articleRepository) 
        {
            _articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<ApiResponse<List<Article>>> Get() 
        {
            return new() 
            {
                Result = await _articleRepository.GetAllArticles()
            };
        }
    }
}