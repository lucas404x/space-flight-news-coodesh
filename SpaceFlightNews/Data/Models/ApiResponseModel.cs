namespace SpaceFlightNews.Data.Models
{
    public class ApiResponse<T>
    {
        public T? Result { get; set; }
        public long ElapsedTimeInMilliseconds { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}