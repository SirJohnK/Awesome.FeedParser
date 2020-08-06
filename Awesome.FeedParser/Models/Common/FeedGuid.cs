using System;

namespace Awesome.FeedParser.Models.Common
{
    public class FeedGuid
    {
        public bool? IsPermaLink { get; internal set; }
        public string? Guid { get; internal set; }
        public Uri? Link { get; internal set; }
    }
}