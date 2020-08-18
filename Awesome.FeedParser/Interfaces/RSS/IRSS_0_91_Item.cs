using System;

namespace Awesome.FeedParser.Interfaces.RSS
{
    /// <summary>
    /// Interface to access RSS 0.91 specified feed items.
    /// </summary>
    public interface IRSS_0_91_Item
    {
        #region Required

        /// <summary>
        /// The URL to the HTML website corresponding to the feed item.
        /// </summary>
        public Uri? Link { get; }

        /// <summary>
        /// The name of the feed item.
        /// </summary>
        public string? Title { get; }

        #endregion Required

        #region Optional

        /// <summary>
        /// Phrase or sentence describing the feed item.
        /// </summary>
        public string? Description { get; }

        #endregion Optional
    }
}