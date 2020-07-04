using Awesome.FeedParser.Models;
using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access RSS 0.92 specified properties
    /// </summary>
    public interface IRSS_0_92_Feed : IRSS_0_91_Feed
    {
        #region Optional

        /// <summary>
        /// Allows processes to register with a cloud to be notified of updates to the feed, implementing a lightweight publish-subscribe protocol for feeds.
        /// </summary>
        public FeedCloud? Cloud { get; }

        /// <summary>
        /// RSS 0.92 feed items
        /// </summary>
        public new IEnumerable<IRSS_0_92_Item> Items { get; }

        #endregion Optional
    }
}