using MongoDB.Driver;
using SpaceFlightNews.Data.Entities;
using SpaceFlightNews.Infrastructure.Database;

namespace SpaceFlightNews.Infrastructure.Repositories
{
    public interface IArticleRepository 
    {
        Task<List<Article>> GetAllArticles();
    }

    public class ArticleRepository: IArticleRepository 
    {
        private readonly IMongoCollection<Article>? _collection;
        public ArticleRepository(IDatabaseContext context) 
        {
            _collection = context.GetCollection<Article>();
        }

        public async Task<List<Article>> GetAllArticles()
        {
            return await _collection.AsQueryable().ToListAsync();
        }
    }
}