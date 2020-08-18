using Awesome.FeedParser.Models.Common;
using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces.Atom
{
    /// <summary>
    /// Interface to access Atom specified feed entries.
    /// </summary>
    public interface IAtomEntry
    {
        #region Required

        /// <summary>
        /// Identifies the feed entry using a universally unique and permanent URI.
        /// </summary>
        public Uri? Id { get; }

        /// <summary>
        /// The name of the feed entry.
        /// </summary>
        public FeedText? Title { get; }

        /// <summary>
        /// Indicates the last time the feed entry was modified in a significant way.
        /// </summary>
        public DateTime? Updated { get; }

        #endregion Required

        #region Optional

        /// <summary>
        /// Names one author of the feed entry.
        /// </summary>
        public FeedPerson? Author { get; }

        /// <summary>
        /// One or more categories that the feed entry belongs to.
        /// </summary>
        public IReadOnlyList<IAtomFeedCategory>? Categories { get; }

        /// <summary>
        /// Contains or links to the complete content of the entry.
        /// </summary>
        public FeedContent? Content { get; }

        /// <summary>
        /// Name of one or more contributors to the feed entry.
        /// </summary>
        public IReadOnlyList<FeedPerson>? Contributors { get; }

        /// <summary>
        /// Links to referenced resources (typically a Web page)
        /// </summary>
        public IReadOnlyList<FeedLink>? Links { get; }

        /// <summary>
        /// Contains the time of the initial creation or first availability of the entry.
        /// </summary>
        public DateTime? Published { get; }

        /// <summary>
        /// Conveys information about rights, e.g. copyrights, held in and over the entry.
        /// </summary>
        public FeedText? Rights { get; }

        /// <summary>
        /// Contains metadata from the source feed if this entry is a copy.
        /// </summary>
        public IAtomEntrySource? Source { get; }

        /// <summary>
        /// Conveys a short summary, abstract, or excerpt of the entry.
        /// </summary>
        public FeedText? Summary { get; }

        #endregion Optional
    }
}