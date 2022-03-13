using SpaceFlightNews.Data.Entities;
using SpaceFlightNews.Infrastructure.Repositories;

namespace SpaceFlightNews.Services 
{
	public interface IArticleServices 
	{
		Task<List<Article>> GetArticles(int limit, int offset);
	}

	public class ArticleServices : IArticleServices
	{
		private readonly IArticleRepository _articleRepository;

		public ArticleServices(IArticleRepository articleRepository) 
		{
			_articleRepository = articleRepository;
		}

		public async Task<List<Article>> GetArticles(int limit, int offset) 
		{
			var articles = await _articleRepository.GetArticlesByOffset(offset);
			return articles.Take(limit).ToList();
		}

	}
}