using System;

namespace Awesome.FeedParser.Models
{
    public class FeedMedia
    {
        public Uri? Url { get; internal set; }
        public long? Length { get; internal set; }
        public string? Type { get; internal set; }
    }
}