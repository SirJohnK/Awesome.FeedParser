using System;

namespace Awesome.FeedParser.Models
{
    public class FeedImage
    {
        public string? Title { get; internal set; }

        public string? Description { get; internal set; }

        public Uri? Url { get; internal set; }

        public Uri? Link { get; internal set; }

        public int? Width { get; internal set; }

        public int? Height { get; internal set; }
    }
}