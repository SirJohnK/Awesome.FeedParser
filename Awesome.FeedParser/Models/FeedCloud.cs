namespace Awesome.FeedParser.Models
{
    public class FeedCloud
    {
        public string? Domain { get; internal set; }
        public string? Port { get; internal set; }
        public string? Path { get; internal set; }
        public string? RegisterProcedure { get; internal set; }
        public string? Protocol { get; internal set; }
    }
}