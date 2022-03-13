namespace SpaceFlightNews.Data.Models
{
    public class ApiResponse<T>
    {
        public T? Result { get; set; }
        public Status? Status { get; set; }
        public long ElapsedTimeInMilliseconds { get; set; }
    }

    public class Status
    {
        public int Code { get; set; }
        public string? Message { get; set; }
    }
}