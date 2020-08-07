using System;

namespace Awesome.FeedParser.Models.Common
{
    /// <summary>
    /// Used to hold string/link that uniquely identifies the feed item.
    /// </summary>
    public class FeedGuid
    {
        /// <summary>
        /// Flag indication if guid is a url.
        /// </summary>
        public bool? IsPermaLink { get; internal set; }

        /// <summary>
        /// Raw guid content.
        /// </summary>
        public string? Guid { get; internal set; }

        /// <summary>
        /// Guid uri link.
        /// </summary>
        public Uri? Link { get; internal set; }
    }
}