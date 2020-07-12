using Awesome.FeedParser.Interfaces;
using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Atom Parser Feed Entry Result Class.
    /// </summary>
    public class AtomEntry : ICommonAtomEntry
    {
        #region Required

        /// <summary>
        /// Identifies the feed entry using a universally unique and permanent URI.
        /// </summary>
        public Uri? Id { get; internal set; }

        /// <summary>
        /// ICommonAtom interface, feed entry implementation of Id.
        /// </summary>
        Uri? ICommonAtom.Id { get => Id; set => Id = value; }

        /// <summary>
        /// The name of the feed entry.
        /// </summary>
        public FeedText? Title { get; internal set; }

        /// <summary>
        /// ICommonAtom interface, feed entry implementation of Title.
        /// </summary>
        FeedText? ICommonAtom.Title { get => Title; set => Title = value; }

        /// <summary>
        /// Indicates the last time the feed entry was modified in a significant way.
        /// </summary>
        public DateTime? Updated { get; internal set; }

        /// <summary>
        /// ICommonAtom interface, feed entry implementation of Updated.
        /// </summary>
        DateTime? ICommonAtom.Updated { get => Updated; set => Updated = value; }

        #endregion Required

        #region Optional

        /// <summary>
        /// Names one author of the feed entry.
        /// </summary>
        public FeedPerson? Author { get; internal set; }

        /// <summary>
        /// ICommonAtom interface, feed entry implementation of Author.
        /// </summary>
        FeedPerson? ICommonAtom.Author { get => Author; set => Author = value; }

        /// <summary>
        /// Internal list of categories for parser access
        /// </summary>
        internal List<FeedCategory>? categories;

        /// <summary>
        /// One or more categories that the feed entry belongs to.
        /// </summary>
        public IReadOnlyList<FeedCategory>? Categories => categories;

        /// <summary>
        /// ICommonAtom interface, feed implementation of Categories.
        /// </summary>
        List<FeedCategory>? ICommonAtom.Categories { get => categories; set => categories = value; }

        /// <summary>
        /// Internal list of contributors for parser access
        /// </summary>
        internal List<FeedPerson>? contributors;

        /// <summary>
        /// Name of one or more contributors to the feed entry.
        /// </summary>
        public IReadOnlyList<FeedPerson>? Contributors => contributors;

        /// <summary>
        /// ICommonAtom interface, feed implementation of Contributors.
        /// </summary>
        List<FeedPerson>? ICommonAtom.Contributors { get => contributors; set => contributors = value; }

        /// <summary>
        /// Internal list of links for parser access
        /// </summary>
        internal List<FeedLink>? links;

        /// <summary>
        /// Links to referenced resources (typically a Web page)
        /// </summary>
        public IReadOnlyList<FeedLink>? Links => links;

        /// <summary>
        /// ICommonAtom interface, feed entry implementation of Link.
        /// </summary>
        List<FeedLink>? ICommonAtom.Links { get => links; set => links = value; }

        /// <summary>
        /// Conveys a short summary, abstract, or excerpt of the entry.
        /// </summary>
        public FeedText? Summary { get; internal set; }

        /// <summary>
        /// ICommonAtomEntry interface, feed entry implementation of Summary.
        /// </summary>
        FeedText? ICommonAtomEntry.Summary { get => Summary; set => Summary = value; }

        #endregion Optional
    }
}