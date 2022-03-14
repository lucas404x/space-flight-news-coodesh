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
		Task<bool> DeleteArticle(string id);
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
			if (limit <= 0) 
			{
				throw new ArgumentException("Limit cannot be equal or lower than 0");
			}

			if (offset < 0) 
			{
				throw new ArgumentException("Offset cannot be lower than 0");
			}

			var articles = await _articleRepository.GetArticlesByOffset(offset);
			return articles.Take(limit).ToList();
		}

        public async Task<Article> GetArticle(string id)
        {
			if (string.IsNullOrEmpty(id)) 
			{
				throw new ArgumentException("Id cannot be null nor empty");
			}

			var article = await _articleRepository.GetArticle(id);
			if (article == null)
			{
				throw new NullReferenceException("Article not found");
			}
			
			return article;
        }

		public async Task<bool> AddArticle(UserArticle article) 
		{
			if (article == null) 
			{
				throw new ArgumentNullException("Article cannot be null");
			}

			var articleNum = await _articleRepository.GetUserArticlesCount() + 1;
			return await _articleRepository.AddArticle(new(article, articleNum));
		}

		public async Task<bool> UpdateArticle(string id, UserArticle userArticle) 
		{
			if (string.IsNullOrEmpty(id)) 
			{
				throw new ArgumentException("Id cannot be null nor empty");
			}

			if (userArticle == null) 
			{
				throw new ArgumentException("Article cannot be null");
			}

			Article? dbArticle = await _articleRepository.GetArticle(id);
			if (dbArticle == null) 
			{
				throw new NullReferenceException("Article doesn't exists.");
			}
			
			Article updatedArticle = new(userArticle, dbArticle.ArticleNum);
			updatedArticle.ArticleNum = dbArticle.ArticleNum;
			updatedArticle.Origin = dbArticle.Origin;
			updatedArticle.Id = dbArticle.Id;

			return await _articleRepository.UpdateArticle(updatedArticle);
		}

		public async Task<bool> DeleteArticle(string id) 
		{
			if (string.IsNullOrEmpty(id)) 
			{
				throw new ArgumentException("Id cannot be null nor empty");
			}

			return await _articleRepository.DeleteArticle(id);
		}
	}
}