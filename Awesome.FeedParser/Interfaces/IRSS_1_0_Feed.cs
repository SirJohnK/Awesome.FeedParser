using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access RSS 1.0 specified properties
    /// </summary>
    public interface IRSS_1_0_Feed : IRSS_0_92_Feed
    {
        #region Required

        /// <summary>
        /// Url to information about feed.
        /// </summary>
        public Uri? About { get; }

        #endregion Required

        #region Optional

        /// <summary>
        /// An RDF Sequence is used to contain all the items to denote item order for rendering and reconstruction.
        /// </summary>
        public IEnumerable<Uri>? ItemsSequence { get; }

        /// <summary>
        /// RSS 1.0 feed items
        /// </summary>
        public new IEnumerable<IRSS_1_0_Item> Items { get; }

        #endregion Optional
    }
}