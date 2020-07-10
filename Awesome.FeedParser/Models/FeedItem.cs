using Awesome.FeedParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Main Feed Parser Item Result Class.
    /// </summary>
    public class FeedItem : IRSS_0_91_Item, IRSS_0_92_Item, IRSS_1_0_Item, IRSS_2_0_Item, IAtomEntry, ICommonFeed
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
        public string? Description { get; internal set; }

        /// <summary>
        /// ICommon interface, feed item implementation of Description.
        /// </summary>
        string? ICommonFeed.Description { get => Description; set => Description = value; }

        /// <summary>
        /// The name of the feed item.
        /// </summary>
        public string? Title { get; internal set; }

        /// <summary>
        /// ICommon interface, feed item implementation of Title.
        /// </summary>
        string? ICommonFeed.Title { get => Title; set => Title = value; }

        #endregion Required

        #region Optional

        /// <summary>
        /// Email address of the author of the feed item.
        /// </summary>
        public MailAddress? Author { get; internal set; }

        /// <summary>
        /// Internal list of categories for parser access
        /// </summary>
        internal List<FeedCategory>? categories;

        /// <summary>
        /// One or more categories that the feed item belongs to.
        /// </summary>
        public IReadOnlyList<FeedCategory>? Categories => categories;

        /// <summary>
        /// ICommon interface, feed item implementation of Categories.
        /// </summary>
        List<FeedCategory>? ICommonFeed.Categories { get => categories; set => categories = value; }

        /// <summary>
        /// URL of a page for comments relating to the feed item.
        /// </summary>
        public Uri? Comments { get; internal set; }

        /// <summary>
        /// Media object that is attached to the feed item.
        /// </summary>
        public FeedMedia? Enclosure { get; internal set; }

        /// <summary>
        /// A string/link that uniquely identifies the feed item.
        /// </summary>
        public FeedGuid? Guid { get; internal set; }

        /// <summary>
        /// The URL to the HTML website corresponding to the feed item.
        /// </summary>
        public Uri? Link { get; internal set; }

        /// <summary>
        /// ICommon interface, feed item implementation of Link.
        /// </summary>
        Uri? ICommonFeed.Link { get => Link; set => Link = value; }

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

        #endregion Optional

        #region Extended Namespaces

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