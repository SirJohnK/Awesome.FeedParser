using System.Collections.Generic;

namespace Awesome.FeedParser.Models
{
    public class ITunesFeed
    {
        public string? Author { get; set; }
        public bool? Block { get; set; }
        public Dictionary<string, IEnumerable<string>>? Category { get; set; }
        public bool? Explicit { get; set; }
        public string? Image { get; set; }
        public IEnumerable<string>? Keywords { get; set; }
        public string? NewFeedUrl { get; set; }
        public string? Owner { get; set; }
        public string? Subtitle { get; set; }
        public string? Summary { get; set; }
    }
}