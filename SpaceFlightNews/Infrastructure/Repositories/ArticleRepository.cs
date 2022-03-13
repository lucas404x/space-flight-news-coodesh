using MongoDB.Driver;
using SpaceFlightNews.Data.Entities;
using SpaceFlightNews.Data.Enumerators;
using SpaceFlightNews.Infrastructure.Database;
using MongoDB.Bson;

namespace SpaceFlightNews.Infrastructure.Repositories
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetArticlesByOffset(int offset);
        Task<Article?> GetArticle(string id);
        Task<int> GetUserArticlesCount();
        Task<bool> AddArticle(Article article);
        Task<bool> UpdateArticle(Article updatedArticle);
        Task<bool> DeleteArticle(string id);
    }

    public class ArticleRepository : IArticleRepository
    {
        private readonly IMongoCollection<Article> _collection;
        public ArticleRepository(IDatabaseContext context)
        {
            _collection = context.GetCollection<Article>();
        }

        public async Task<List<Article>> GetArticlesByOffset(int offset)
        {
            return await Task.FromResult(_collection.AsQueryable().Skip(offset).ToList());
        }

        public async Task<Article?> GetArticle(string id)
        {
            var article = await _collection.FindAsync(
                (article) => article.Id == new ObjectId(id)
            );

            return article.FirstOrDefault();
        }

        public async Task<int> GetUserArticlesCount()
        {
            var allUserArticles = await _collection.FindAsync(
                (article) => article.Origin == ArticleOrigin.USER
            );

            return allUserArticles.ToList().Count;
        }

        public async Task<bool> AddArticle(Article article)
        {
            await _collection.InsertOneAsync(article);
            return true;
        }

        public async Task<bool> UpdateArticle(Article updatedArticle)
        {
            await _collection.FindOneAndReplaceAsync(
                (article) => article.Id == updatedArticle.Id,
                updatedArticle
            );

            return true;
        }

        public async Task<bool> DeleteArticle(string id)
        {
            await _collection.FindOneAndDeleteAsync((article) => article.Id == new ObjectId(id));
            return true;
        }
    }
}