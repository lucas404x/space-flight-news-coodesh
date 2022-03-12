using MongoDB.Driver;
using RestSharp;
using SpaceFlightNews.Data.Entities;
using SpaceFlightNews.Data.Models;

IMongoDatabase GetDatabase(string database)
{
    const string connectionString = "mongodb+srv://noti0nS:lugty321@space-flight-news-coode.xrlqa.mongodb.net/myFirstDatabase?retryWrites=true&w=majority";
    var client = new MongoClient(connectionString);
    return client.GetDatabase(database);
}

async IAsyncEnumerable<ApiArticle[]> RequestArticles(RestClient client, int articlesNo)
{
    const int maximumArticlesPerRequest = 50;
    int lastArticleId = 0;
    int offset = 0;

    RestRequest request;
    RestResponse<ApiArticle[]> response;
    while (lastArticleId < articlesNo)
    {
        request = new("articles");
        request.AddParameter("_sort", "id");
        request.AddParameter("_limit", maximumArticlesPerRequest);
        request.AddParameter("_start", offset);
        response = await client.ExecuteGetAsync<ApiArticle[]>(request);
        if (response.Data != null)
        {
            lastArticleId = response.Data.Last().Id;
            yield return response.Data;
        }

        offset += maximumArticlesPerRequest;
    }

    yield break;
}

Article[] ConvertApiArticlesToArticles(ApiArticle[] apiArticles)
{
    var articles = new Article[apiArticles.Length];

    for (int i = 0; i < articles.Length; i++)
    {
        articles[i] = new(apiArticles[i]);
    }

    return articles;
}

async Task StoreArticlesInDatabase(IMongoDatabase database, Article[] articles)
{
    var collection = database.GetCollection<Article>("Article");
    await collection.InsertManyAsync(articles);
}

const string baseURL = "https://api.spaceflightnewsapi.net/v3";

RestClient client = new(baseURL);
RestRequest request = new("articles/count");
RestResponse response = await client.ExecuteGetAsync(request);

if (int.TryParse(response.Content, out int articlesNo))
{
    var database = GetDatabase("Space-Flight-News-Coodesh");
    CreateIndexModel<Article> indexModel = new(Builders<Article>.IndexKeys
        .Ascending((field) => field.Origin));
    await database.GetCollection<Article>("Article").Indexes
        .CreateOneAsync(indexModel);
    
    await foreach (var articles in RequestArticles(client, articlesNo))
    {
        await StoreArticlesInDatabase(database, ConvertApiArticlesToArticles(articles));
        Console.WriteLine(articles.Last().Id);
    }
}