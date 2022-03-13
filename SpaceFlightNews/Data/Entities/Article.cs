using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SpaceFlightNews.Data.Enumerators;
using SpaceFlightNews.Data.Models;

namespace SpaceFlightNews.Data.Entities 
{
    public class Article : BaseEntity 
    {
        public int ArticleNum { get; set; }
        [BsonRepresentation(BsonType.String)]
        public ArticleOrigin Origin { get; set; }
        public bool Featured { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
        public string? NewsSite { get; set; }
        public string? Summary { get; set; }
        public DateTime? PublishedAt { get; set; }
        public List<ContentProvider>? Launches { get; set; }
        public List<ContentProvider>? Events { get; set; }

        public Article() {}

        public Article(ApiArticle article) 
        {
            ArticleNum = article.Id;
            Origin = ArticleOrigin.API;
            Featured = article.Featured;
            Title = article.Title;
            Url = article.Url;
            ImageUrl = article.ImageUrl;
            NewsSite = article.NewsSite;
            Summary = article.Summary;
            PublishedAt = article.PublishedAt;
            Launches = article.Launches;
            Events = article.Events;
        }

        public Article(UserArticle article, int articleNum) 
        {
            ArticleNum = articleNum;
            Origin = ArticleOrigin.USER;
            Featured = article.Featured;
            Title = article.Title;
            Url = article.Url;
            ImageUrl = article.ImageUrl;
            NewsSite = article.NewsSite;
            Summary = article.Summary;
            PublishedAt = article.PublishedAt;
            Launches = article.Launches;
            Events = article.Events;
        }
    }

    public class ContentProvider
    {
        public string? Id { get; set; }
        public string? Provider { get; set; }
    }
}