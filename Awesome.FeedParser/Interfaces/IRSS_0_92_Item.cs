using Awesome.FeedParser.Models;
using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access RSS 0.92 specified feed items.
    /// </summary>
    public interface IRSS_0_92_Item : IRSS_0_91_Item
    {
        #region Optional

        /// <summary>
        /// One or more categories that the feed item belongs to.
        /// </summary>
        public IReadOnlyList<ICommonFeedCategory>? Categories { get; }

        /// <summary>
        /// Media object that is attached to the feed item.
        /// </summary>
        public IEnclosure? Enclosure { get; }

        /// <summary>
        /// The feed that the feed item came from.
        /// </summary>
        public FeedLink? Source { get; }

        #endregion Optional
    }
}