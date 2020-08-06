using Awesome.FeedParser.Models.Common;
using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces.Atom
{
    /// <summary>
    /// Interface to access Atom feed specified properties
    /// </summary>
    public interface IAtomFeed
    {
        #region Required

        /// <summary>
        /// Identifies the feed using a universally unique and permanent URI.
        /// </summary>
        public Uri? Id { get; }

        /// <summary>
        /// The name of the feed.
        /// </summary>
        public FeedText? Title { get; }

        /// <summary>
        /// Indicates the last time the feed was modified in a significant way.
        /// </summary>
        public DateTime? Updated { get; }

        #endregion Required

        #region Optional

        /// <summary>
        /// Names one author of the feed.
        /// </summary>
        public FeedPerson? Author { get; }

        /// <summary>
        /// One or more categories that the feed belongs to.
        /// </summary>
        public IReadOnlyList<IAtomFeedCategory>? Categories { get; }

        /// <summary>
        /// Name of one or more contributors to the feed entry.
        /// </summary>
        public IReadOnlyList<FeedPerson>? Contributors { get; }

        /// <summary>
        /// Atom feed entries.
        /// </summary>
        public IEnumerable<IAtomEntry> Entries { get; }

        /// <summary>
        /// Indicating the program used to generate the feed.
        /// </summary>
        public FeedGenerator? Generator { get; }

        /// <summary>
        /// Identifies a small image which provides iconic visual identification for the feed.
        /// </summary>
        public Uri? Icon { get; }

        /// <summary>
        /// Links to referenced resources (typically a Web page)
        /// </summary>
        public IReadOnlyList<FeedLink>? Links { get; }

        /// <summary>
        /// Identifies a larger image which provides visual identification for the feed.
        /// </summary>
        public Uri? Logo { get; }

        /// <summary>
        /// Conveys information about rights, e.g. copyrights, held in and over the feed.
        /// </summary>
        public FeedText? Rights { get; }

        /// <summary>
        /// Contains a human-readable description or subtitle for the feed.
        /// </summary>
        public FeedText? Subtitle { get; }

        #endregion Optional
    }
}