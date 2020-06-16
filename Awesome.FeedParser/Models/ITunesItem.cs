using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Models
{
    public class ITunesItem
    {
        public string? Author { get; set; }
        public bool? Block { get; set; }
        public TimeSpan? Duration { get; set; }
        public bool? Explicit { get; set; }
        public string? Image { get; set; }
        public IEnumerable<string>? Keywords { get; set; }
        public DateTime? PubDate { get; set; }
        public string? Subtitle { get; set; }
        public string? Summary { get; set; }
    }
}