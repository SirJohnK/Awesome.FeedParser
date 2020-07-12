﻿using Awesome.FeedParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Main Feed Parser Item Result Class.
    /// </summary>
    public class FeedItem : IRSS_0_91_Item, IRSS_0_92_Item, IRSS_1_0_Item, IRSS_2_0_Item, IAtomEntry, ICommonFeed, ICommonAtomEntry
    {
        #region Feed Parser

        /// <summary>
        /// Parsed feed item content types (enum flags)
        /// </summary>
        public FeedContentType ContentType { get; internal set; } = FeedContentType.Basic;

        /// <summary>
        /// ICommon interface, feed item implementation of ContentType.
        /// </summary>
        FeedContentType ICommonFeed.ContentType { get => ContentType; set => ContentType = value; }

        #endregion Feed Parser

        #region Required

        /// <summary>
        /// Url to information about feed item. (RSS 1.0 Only)
        /// </summary>
        public Uri? About { get; internal set; }

        /// <summary>
        /// Phrase or sentence describing the feed item (entity-encoded HTML is allowed).
        /// </summary>
        public FeedText? Description { get; internal set; }

        /// <summary>
        /// IRSS_0_91_Item interface, feed item implementation of Description.
        /// </summary>
        string? IRSS_0_91_Item.Description { get => Description?.Text; }

        /// <summary>
        /// ICommon interface, feed item implementation of Description.
        /// </summary>
        FeedText? ICommonFeed.Description { get => Description; set => Description = value; }

        /// <summary>
        /// Identifies the feed item using a universally unique and permanent URI. (Atom only)
        /// </summary>
        public Uri? Id { get; internal set; }

        /// <summary>
        /// ICommonAtom interface, feed item implementation of Id.
        /// </summary>
        Uri? ICommonAtom.Id { get => Id; set => Id = value; }

        /// <summary>
        /// The name of the feed item.
        /// </summary>
        public FeedText? Title { get; internal set; }

        /// <summary>
        /// IRSS_0_91_Item interface, feed item implementation of Title.
        /// </summary>
        string? IRSS_0_91_Item.Title { get => Title?.Text; }

        /// <summary>
        /// ICommon interface, feed item implementation of Title.
        /// </summary>
        FeedText? ICommonFeed.Title { get => Title; set => Title = value; }

        /// <summary>
        /// ICommonAtom interface, feed item implementation of Title.
        /// </summary>
        FeedText? ICommonAtom.Title { get => Title; set => Title = value; }

        /// <summary>
        /// Indicates the last time the feed item was modified in a significant way. (Atom only)
        /// </summary>
        public DateTime? Updated { get; internal set; }

        /// <summary>
        /// ICommonAtom interface, feed item implementation of Updated.
        /// </summary>
        DateTime? ICommonAtom.Updated { get => Updated; set => Updated = value; }

        #endregion Required

        #region Optional

        /// <summary>
        /// Author information of the feed item.
        /// </summary>
        public FeedPerson? Author { get; internal set; }

        /// <summary>
        /// IRSS_2_0_Item interface, feed item implementation of Author.
        /// </summary>
        MailAddress? IRSS_2_0_Item.Author { get => Author?.Email; }

        /// <summary>
        /// ICommonAtom interface, feed item implementation of Author.
        /// </summary>
        FeedPerson? ICommonAtom.Author { get => Author; set => Author = value; }

        /// <summary>
        /// Internal list of categories for parser access
        /// </summary>
        internal List<FeedCategory>? categories;

        /// <summary>
        /// One or more categories that the feed item belongs to.
        /// </summary>
        public IReadOnlyList<FeedCategory>? Categories => categories;

        /// <summary>
        /// IRSS_0_92_Item interface, feed item implementation of Categories.
        /// </summary>
        IReadOnlyList<ICommonFeedCategory>? IRSS_0_92_Item.Categories { get => Categories; }

        /// <summary>
        /// ICommon interface, feed item implementation of Categories.
        /// </summary>
        List<FeedCategory>? ICommonFeed.Categories { get => categories; set => categories = value; }

        /// <summary>
        /// IAtomEntry interface, feed item implementation of Categories.
        /// </summary>
        IReadOnlyList<IAtomFeedCategory>? IAtomEntry.Categories { get => Categories; }

        /// <summary>
        /// ICommonAtom interface, feed implementation of Categories.
        /// </summary>
        List<FeedCategory>? ICommonAtom.Categories { get => categories; set => categories = value; }

        /// <summary>
        /// URL of a page for comments relating to the feed item.
        /// </summary>
        public Uri? Comments { get; internal set; }

        /// <summary>
        /// Internal list of contributors for parser access
        /// </summary>
        internal List<FeedPerson>? contributors;

        /// <summary>
        /// Name of one or more contributors to the feed item. (Atom only)
        /// </summary>
        public IReadOnlyList<FeedPerson>? Contributors => contributors;

        /// <summary>
        /// ICommonAtom interface, feed implementation of Contributors.
        /// </summary>
        List<FeedPerson>? ICommonAtom.Contributors { get => contributors; set => contributors = value; }

        /// <summary>
        /// Media object that is attached to the feed item.
        /// </summary>
        public FeedMedia? Enclosure { get; internal set; }

        /// <summary>
        /// A string/link that uniquely identifies the feed item.
        /// </summary>
        public FeedGuid? Guid { get; internal set; }

        /// <summary>
        /// Internal list of links for parser access
        /// </summary>
        internal List<FeedLink>? links;

        /// <summary>
        /// Links to referenced resources (typically a Web page)
        /// </summary>
        public IReadOnlyList<FeedLink>? Links => links;

        /// <summary>
        /// IRSS_0_91_Item interface, feed item implementation of Link.
        /// </summary>
        Uri? IRSS_0_91_Item.Link { get => Links?.FirstOrDefault()?.Url; }

        /// <summary>
        /// ICommon interface, feed item implementation of Link.
        /// </summary>
        List<FeedLink>? ICommonFeed.Links { get => links; set => links = value; }

        /// <summary>
        /// ICommonAtom interface, feed item implementation of Link.
        /// </summary>
        List<FeedLink>? ICommonAtom.Links { get => links; set => links = value; }

        /// <summary>
        /// The publication date for the content in the feed item.
        /// </summary>
        public DateTime? PubDate { get; internal set; }

        /// <summary>
        /// ICommon interface, feed item implementation of PubDate.
        /// </summary>
        DateTime? ICommonFeed.PubDate { get => PubDate; set => PubDate = value; }

        /// <summary>
        /// The feed that the feed item came from.
        /// </summary>
        public FeedLink? Source { get; internal set; }

        /// <summary>
        /// IAtomEntry interface, feed item implementation of Summary.
        /// </summary>
        FeedText? IAtomEntry.Summary { get => Description; }

        /// <summary>
        /// ICommonAtomEntry interface, feed item implementation of Summary.
        /// </summary>
        FeedText? ICommonAtomEntry.Summary { get => Description; set => Description = value; }

        #endregion Optional

        #region Extended Namespaces

        /// <summary>
        /// Flag indicatig if feed item has Atom information.
        /// </summary>
        public bool HasAtom => Atom != null;

        /// <summary>
        /// The Atom specific feed item information.
        /// </summary>
        public AtomEntry? Atom { get; internal set; }

        /// <summary>
        /// Flag indicatig if feed item has iTunes information.
        /// </summary>
        public bool HasITunes => ITunes != null;

        /// <summary>
        /// The iTunes specific feed item information.
        /// </summary>
        public ITunesItem? ITunes { get; internal set; }

        #endregion Extended Namespaces
    }
}