using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace SpaceFlightNews.Infrastructure.Database 
{
    public interface IDatabaseContext 
    {
        IMongoCollection<T> GetCollection<T>(); 
    }

    public class DatabaseContext : IDatabaseContext
    {
        private readonly IMongoDatabase _database;
        public DatabaseContext(IOptions<DatabaseSettings> settings) 
        {
            MongoClient mongoClient = new(settings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }
    }
}