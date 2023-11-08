namespace P7CreateRestApi.Models
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public string StatusCode { get; internal set; }
        public string? Description { get; internal set; }
    }
}
