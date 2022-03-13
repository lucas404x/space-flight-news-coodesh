using RestSharp;
using Coravel.Invocable;
using SpaceFlightNews.Data.Models;
using SpaceFlightNews.Data.Entities;
using SpaceFlightNews.Data.Enumerators;
using SpaceFlightNews.Infrastructure.Repositories;

namespace SpaceFlightNews.Invocables
{
    public class GetDailyNewsJob : IInvocable
    {
        private const string baseURL = "https://api.spaceflightnewsapi.net/v3";
        private readonly ILogger<GetDailyNewsJob> _logger;
        private readonly IArticleRepository _articleRepository;

        public GetDailyNewsJob(ILogger<GetDailyNewsJob> logger, IArticleRepository articleRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
        }

        public async Task Invoke()
        {
            _logger.LogInformation("GetDailyNews Method fired! 🔥");
            RestClient client = new(baseURL);
            RestRequest request = new("articles");
            RestResponse<ApiArticle[]> response = await client.ExecuteGetAsync<ApiArticle[]>(request);
            if (response.Data != null)
            {
                foreach (var apiArticle in response.Data)
                {
                    Article? article = await _articleRepository.FindArticleByOriginAndNumber(
                        ArticleOrigin.API, apiArticle.Id
                    );
                    
                    if (article == null) 
                    {
                        await _articleRepository.AddArticle(new(apiArticle));
                        _logger.LogInformation($"New article added: {apiArticle.Id}::{apiArticle.Title}");
                    }
                }
            }
            
            _logger.LogInformation("GetDailyNews finish 🔥");
        }
    }
}