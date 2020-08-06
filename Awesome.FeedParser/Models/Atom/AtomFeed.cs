using Awesome.FeedParser.Interfaces.Atom;
using Awesome.FeedParser.Models.Common;
using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Models.Atom
{
    /// <summary>
    /// Atom Parser Feed Result Class.
    /// </summary>
    /// <remarks>
    /// Only used to store Atom feed information for NON Atom feeds!!!
    /// </remarks>
    public class AtomFeed : ICommonAtomFeed
    {
        #region Required

        /// <summary>
        /// Identifies the feed using a universally unique and permanent URI.
        /// </summary>
        public Uri? Id { get; internal set; }

        /// <summary>
        /// ICommonAtom interface, feed implementation of Id.
        /// </summary>
        Uri? ICommonAtom.Id { get => Id; set => Id = value; }

        /// <summary>
        /// The name of the feed.
        /// </summary>
        public FeedText? Title { get; internal set; }

        /// <summary>
        /// ICommonAtom interface, feed implementation of Title.
        /// </summary>
        FeedText? ICommonAtom.Title { get => Title; set => Title = value; }

        /// <summary>
        /// Indicates the last time the feed was modified in a significant way.
        /// </summary>
        public DateTime? Updated { get; internal set; }

        /// <summary>
        /// ICommonAtom interface, feed implementation of Updated.
        /// </summary>
        DateTime? ICommonAtom.Updated { get => Updated; set => Updated = value; }

        #endregion Required

        #region Optional

        /// <summary>
        /// Names one author of the feed.
        /// </summary>
        public FeedPerson? Author { get; internal set; }

        /// <summary>
        /// ICommonAtom interface, feed implementation of Author.
        /// </summary>
        FeedPerson? ICommonAtom.Author { get => Author; set => Author = value; }

        /// <summary>
        /// Internal list of categories for parser access
        /// </summary>
        internal List<FeedCategory>? categories;

        /// <summary>
        /// One or more categories that the feed belongs to.
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
        /// Name of one or more contributors to the feed.
        /// </summary>
        public IReadOnlyList<FeedPerson>? Contributors => contributors;

        /// <summary>
        /// ICommonAtom interface, feed implementation of Contributors.
        /// </summary>
        List<FeedPerson>? ICommonAtom.Contributors { get => contributors; set => contributors = value; }

        /// <summary>
        /// Indicating the program used to generate the feed.
        /// </summary>
        public FeedGenerator? Generator { get; internal set; }

        /// <summary>
        /// ICommonAtomFeed interface, feed implementation of Generator.
        /// </summary>
        FeedGenerator? ICommonAtomFeed.Generator { get => Generator; set => Generator = value; }

        /// <summary>
        /// Identifies a small image which provides iconic visual identification for the feed.
        /// </summary>
        public Uri? Icon { get; internal set; }

        /// <summary>
        /// ICommonAtomFeed interface, feed implementation of Icon.
        /// </summary>
        Uri? ICommonAtomFeed.Icon { get => Icon; set => Icon = value; }

        /// <summary>
        /// Internal list of links for parser access
        /// </summary>
        internal List<FeedLink>? links;

        /// <summary>
        /// Links to referenced resources (typically a Web page)
        /// </summary>
        public IReadOnlyList<FeedLink>? Link => links;

        /// <summary>
        /// ICommonAtom interface, feed implementation of Link.
        /// </summary>
        List<FeedLink>? ICommonAtom.Links { get => links; set => links = value; }

        /// <summary>
        /// Identifies a larger image which provides visual identification for the feed.
        /// </summary>
        public FeedImage? Logo { get; internal set; }

        /// <summary>
        /// ICommonAtomFeed interface, feed implementation of Logo.
        /// </summary>
        FeedImage? ICommonAtomFeed.Logo { get => Logo; set => Logo = value; }

        /// <summary>
        /// Conveys information about rights, e.g. copyrights, held in and over the feed.
        /// </summary>
        public FeedText? Rights { get; internal set; }

        /// <summary>
        /// ICommonAtom interface, feed implementation of Rights.
        /// </summary>
        FeedText? ICommonAtom.Rights { get => Rights; set => Rights = value; }

        /// <summary>
        /// Contains a human-readable description or subtitle for the feed.
        /// </summary>
        public FeedText? Subtitle { get; internal set; }

        /// <summary>
        /// ICommonAtomFeed interface, feed implementation of Subtitle.
        /// </summary>
        FeedText? ICommonAtomFeed.Subtitle { get => Subtitle; set => Subtitle = value; }

        #endregion Optional
    }
}