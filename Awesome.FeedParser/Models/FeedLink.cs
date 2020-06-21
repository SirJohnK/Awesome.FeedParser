using System;

namespace Awesome.FeedParser.Models
{
    public class FeedLink
    {
        public string? Text { get; internal set; }

        public Uri? Url { get; internal set; }
    }
}