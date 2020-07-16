using Awesome.FeedParser.Models;
using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access common feed, Atom feed, feed item and Atom feed entry properties.
    /// </summary>
    internal interface ICommonAtom
    {
        #region Required

        /// <summary>
        /// Identifies the feed/entry using a universally unique and permanent URI.
        /// </summary>
        internal Uri? Id { get; set; }

        /// <summary>
        /// The name of the feed/entry.
        /// </summary>
        internal FeedText? Title { get; set; }

        /// <summary>
        /// Indicates the last time the feed/entry was modified in a significant way.
        /// </summary>
        internal DateTime? Updated { get; set; }

        #endregion Required

        #region Optional

        /// <summary>
        /// Names one author of the feed/entry.
        /// </summary>
        internal FeedPerson? Author { get; set; }

        /// <summary>
        /// One or more categories that the feed/entry belongs to.
        /// </summary>
        internal List<FeedCategory>? Categories { get; set; }

        /// <summary>
        /// Name of one or more contributors to the feed/entry.
        /// </summary>
        internal List<FeedPerson>? Contributors { get; set; }

        /// <summary>
        /// Links to referenced resources (typically a Web page)
        /// </summary>
        internal List<FeedLink>? Links { get; set; }

        /// <summary>
        /// Conveys information about rights, e.g. copyrights, held in and over the feed/entry.
        /// </summary>
        internal FeedText? Rights { get; set; }

        #endregion Optional
    }
}