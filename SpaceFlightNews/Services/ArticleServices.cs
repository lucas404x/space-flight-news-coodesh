using SpaceFlightNews.Data.Entities;
using SpaceFlightNews.Data.Models;
using SpaceFlightNews.Infrastructure.Repositories;

namespace SpaceFlightNews.Services 
{
	public interface IArticleServices 
	{
		Task<List<Article>> GetArticles(int limit, int offset);
		Task<Article> GetArticle(string id);
		Task<bool> AddArticle(UserArticle article);
		Task<bool> UpdateArticle(string id, UserArticle article);
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

        public async Task<Article> GetArticle(string id)
        {
			var article = await _articleRepository.GetArticle(id); 
			if (article == null) 
			{
				throw new NullReferenceException();
			}
			
			return article;
        }

		public async Task<bool> AddArticle(UserArticle article) 
		{
			var articleNum = await _articleRepository.GetUserArticlesCount() + 1;
			return await _articleRepository.AddArticle(new(article, articleNum));
		}

		public async Task<bool> UpdateArticle(string id, UserArticle userArticle) 
		{
			Article? dbArticle = await _articleRepository.GetArticle(id);
			if (dbArticle == null) 
			{
				throw new NullReferenceException();
			}
			
			Article updatedArticle = new(userArticle, dbArticle.ArticleNum);
			updatedArticle.ArticleNum = dbArticle.ArticleNum;
			updatedArticle.Origin = dbArticle.Origin;
			updatedArticle.Id = dbArticle.Id;

			return await _articleRepository.UpdateArticle(updatedArticle);
		}
	}
}