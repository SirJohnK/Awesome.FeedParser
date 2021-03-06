﻿using Awesome.FeedParser.Interfaces.Atom;
using Awesome.FeedParser.Interfaces.Common;
using Awesome.FeedParser.Interfaces.Media;
using System;

namespace Awesome.FeedParser.Models.Common
{
    /// <summary>
    /// Feed or Item category information.
    /// </summary>
    public class FeedCategory : ICommonFeedCategory, IAtomFeedCategory, IMediaCategory
    {
        /// <summary>
        /// Identifies a hierarchic location in the indicated taxonomy.
        /// </summary>
        public string? Category { get; internal set; }

        /// <summary>
        /// Identifies a categorization taxonomy.
        /// </summary>
        public string? Domain { get; internal set; }

        /// <summary>
        /// A human-readable label for display.
        /// </summary>
        public string? Label { get; internal set; }

        /// <summary>
        /// IAtomFeedCategory interface, feed category implementation of Term.
        /// </summary>
        string? IAtomFeedCategory.Term => Category;

        /// <summary>
        /// Identifies the categorization scheme via a URI.
        /// </summary>
        public Uri? Scheme { get; internal set; }
    }
}