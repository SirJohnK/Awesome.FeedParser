using System;

namespace Awesome.FeedParser.Interfaces
{
    public interface ICommonFeed
    {
        public string? Title { get; set; }
        public Uri? Link { get; set; }
        public string? Description { get; set; }
        public DateTime? PubDate { get; set; }
    }
}