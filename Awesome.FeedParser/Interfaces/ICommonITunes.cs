using Awesome.FeedParser.Models;
using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces
{
    internal interface ICommonITunes
    {
        internal string? Author { get; set; }
        internal bool? Block { get; set; }
        internal bool? Explicit { get; set; }
        internal FeedImage? Image { get; set; }
        internal IEnumerable<string>? Keywords { get; set; }
        internal string? Title { get; set; }
        internal string? Subtitle { get; set; }
        internal string? Summary { get; set; }
    }
}