using System;

namespace Awesome.FeedParser.Models.Common
{
    public class FeedTextInput
    {
        /// <summary>
        /// Url to information about text input. (RSS 1.0 Only)
        /// </summary>
        public Uri? About { get; internal set; }

        public string? Description { get; internal set; }

        public Uri? Link { get; internal set; }

        public string? Name { get; internal set; }

        public string? Title { get; internal set; }
    }
}