using SpaceFlightNews.Data.Entities;

namespace SpaceFlightNews.Data.Models
{
    public class ApiArticle
    {
        public int Id { get; set; }
        public bool Featured { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? ImageUrl { get; set; }
        public string? NewsSite { get; set; }
        public string? Summary { get; set; }
        public DateTime? PublishedAt { get; set; }
        public List<ContentProvider<string>>? Launches { get; set; }
        public List<ContentProvider<int>>? Events { get; set; }
    }
}