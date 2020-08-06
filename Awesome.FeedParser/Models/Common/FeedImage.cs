using System;

namespace Awesome.FeedParser.Models.Common
{
    public class FeedImage
    {
        /// <summary>
        /// Url to information about image. (RSS 1.0 Only)
        /// </summary>
        public Uri? About { get; internal set; }

        public string? Title { get; internal set; }

        public string? Description { get; internal set; }

        public Uri? Url { get; internal set; }

        public Uri? Link { get; internal set; }

        public int? Width { get; internal set; }

        public int? Height { get; internal set; }
    }
}