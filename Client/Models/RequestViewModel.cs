namespace Client.Models
{
    public class RequestViewModel
    {
        public HttpMethod Method { get; set; }
        public string Url { get; set; }
        public object? Data { get; set; }
        public string AccessToken { get; set; }
    }
}
