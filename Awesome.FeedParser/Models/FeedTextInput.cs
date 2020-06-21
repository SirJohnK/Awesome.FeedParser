using System;

namespace Awesome.FeedParser.Models
{
    public class FeedTextInput
    {
        public string? Description { get; internal set; }

        public Uri? Link { get; internal set; }

        public string? Name { get; internal set; }

        public string? Title { get; internal set; }
    }
}