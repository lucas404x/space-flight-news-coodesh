using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SpaceFlightNews.Data.Entities;

namespace SpaceFlightNews.Infrastructure.Database 
{
    public interface IDatabaseContext 
    {
        IMongoCollection<T> GetCollection<T>() where T : BaseEntity; 
    }

    public class DatabaseContext : IDatabaseContext
    {
        private readonly IMongoDatabase _database;
        public DatabaseContext(IOptions<DatabaseSettings> settings) 
        {
            MongoClient mongoClient = new(settings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>() where T : BaseEntity
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }
    }
}