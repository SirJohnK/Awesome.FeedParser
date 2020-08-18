using System;

namespace Awesome.FeedParser.Models.Common
{
    /// <summary>
    /// Indicating the program used to generate the feed.
    /// </summary>
    public class FeedGenerator
    {
        /// <summary>
        /// Program used to generate the feed.
        /// </summary>
        public string? Generator { get; internal set; }

        /// <summary>
        /// Generator Uri.
        /// </summary>
        public Uri? Uri { get; internal set; }

        /// <summary>
        /// Generator version.
        /// </summary>
        public string? Version { get; internal set; }
    }
}