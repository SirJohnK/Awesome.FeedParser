using System;

namespace Awesome.FeedParser.Models.Common
{
    public class FeedGenerator
    {
        public string? Generator { get; internal set; }
        public Uri? Uri { get; internal set; }
        public string? Version { get; internal set; }
    }
}