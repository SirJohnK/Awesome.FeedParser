using Awesome.FeedParser.Models;
using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access common feed and feed item properties.
    /// </summary>
    internal interface ICommonFeed
    {
        #region Feed Parser

        /// <summary>
        /// Parsed feed/item content types (enum flags)
        /// </summary>
        internal FeedContentType ContentType { get; set; }

        #endregion Feed Parser

        #region RSS / Atom

        /// <summary>
        /// One or more categories that the feed/item belongs to.
        /// </summary>
        internal List<FeedCategory>? Categories { get; set; }

        /// <summary>
        /// Phrase or sentence describing the feed/item.
        /// </summary>
        internal string? Description { get; set; }

        /// <summary>
        /// The URL to the HTML website corresponding to the feed/item.
        /// </summary>
        internal Uri? Link { get; set; }

        /// <summary>
        /// The publication date for the content in the feed/item.
        /// </summary>
        internal DateTime? PubDate { get; set; }

        /// <summary>
        /// The name of the feed/item.
        /// </summary>
        internal string? Title { get; set; }

        #endregion RSS / Atom
    }
}