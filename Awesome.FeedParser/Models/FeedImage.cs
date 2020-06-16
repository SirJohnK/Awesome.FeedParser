using System;

namespace Awesome.FeedParser.Models
{
    public class FeedImage
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public Uri? Url { get; set; }

        public Uri? Link { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }
    }
}