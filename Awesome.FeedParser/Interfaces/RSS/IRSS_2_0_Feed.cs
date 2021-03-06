﻿using Awesome.FeedParser.Interfaces.Common;
using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces.RSS
{
    /// <summary>
    /// Interface to access RSS 2.0 specified properties
    /// </summary>
    public interface IRSS_2_0_Feed : IRSS_1_0_Feed
    {
        #region Optional

        /// <summary>
        /// One or more categories that the feed belongs to.
        /// </summary>
        public IReadOnlyList<ICommonFeedCategory>? Categories { get; }

        /// <summary>
        /// A string indicating the program used to generate the feed.
        /// </summary>
        public string? Generator { get; }

        /// <summary>
        /// RSS 2.0 feed items
        /// </summary>
        public new IEnumerable<IRSS_2_0_Item> Items { get; }

        /// <summary>
        /// Number of minutes that indicates how long a feed can be cached before refreshing from the source.
        /// </summary>
        public TimeSpan? Ttl { get; }

        #endregion Optional
    }
}