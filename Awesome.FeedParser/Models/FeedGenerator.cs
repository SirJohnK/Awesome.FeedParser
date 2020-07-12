using System;

namespace Awesome.FeedParser.Models
{
    public class FeedGenerator
    {
        public string? Generator { get; internal set; }
        public Uri? Uri { get; internal set; }
        public string? Version { get; internal set; }
    }
}