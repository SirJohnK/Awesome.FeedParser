using Awesome.FeedParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Main Feed Parser Result Class.
    /// </summary>
    public class Feed : IRSS_0_91_Feed, IRSS_0_92_Feed, IRSS_1_0_Feed, IRSS_2_0_Feed, IAtomFeed, ICommonFeed
    {
        /// <summary>
        /// Parsed feed type.
        /// </summary>
        public FeedType Type { get; internal set; } = FeedType.Unknown;

        /// <summary>
        /// Parsed feed content types (enum flags)
        /// </summary>
        public FeedContentType ContentType { get; internal set; } = FeedContentType.Basic;

        public string? About { get; internal set; }

        /// <summary>
        /// The name of the feed.
        /// </summary>
        public string? Title { get; internal set; }

        string? ICommonFeed.Title { get => Title; set => Title = value; }

        /// <summary>
        /// Phrase or sentence describing the feed.
        /// </summary>
        public string? Description { get; internal set; }

        string? ICommonFeed.Description { get => Description; set => Description = value; }

        /// <summary>
        /// The URL to the HTML website corresponding to the feed.
        /// </summary>
        public Uri? Link { get; internal set; }

        Uri? ICommonFeed.Link { get => Link; set => Link = value; }

        /// <summary>
        /// Specifies a GIF, JPEG or PNG image that can be displayed with the feed.
        /// </summary>
        public FeedImage? Image { get; internal set; }

        /// <summary>
        /// The publication date for the content in the feed.
        /// </summary>
        public DateTime? PubDate { get; internal set; }

        DateTime? ICommonFeed.PubDate { get => PubDate; set => PubDate = value; }

        /// <summary>
        /// The last time the content of the feed changed.
        /// </summary>
        public DateTime? LastBuildDate { get; internal set; }

        /// <summary>
        /// The language the feed is written in. (ISO 639)
        /// </summary>
        public CultureInfo? Language { get; internal set; }

        /// <summary>
        /// Copyright notice for content in the feed.
        /// </summary>
        public string? Copyright { get; internal set; }

        /// <summary>
        /// Email address for person responsible for editorial content.
        /// </summary>
        public MailAddress? ManagingEditor { get; internal set; }

        /// <summary>
        /// Email address for person responsible for technical issues relating to the feed.
        /// </summary>
        public MailAddress? WebMaster { get; internal set; }

        /// <summary>
        /// Internal list of categories for parser access
        /// </summary>
        internal List<FeedCategory>? categories;

        /// <summary>
        /// One or more categories that the feed belongs to.
        /// </summary>
        public IReadOnlyList<FeedCategory>? Categories => categories;

        List<FeedCategory>? ICommonFeed.Categories { get => categories; set => categories = value; }

        /// <summary>
        /// A string indicating the program used to generate the feed.
        /// </summary>
        public string? Generator { get; internal set; }

        /// <summary>
        /// A URL that points to the documentation for the format used for the feed.
        /// </summary>
        public Uri? Docs { get; internal set; }

        /// <summary>
        /// Allows processes to register with a cloud to be notified of updates to the feed, implementing a lightweight publish-subscribe protocol for feeds.
        /// </summary>
        public FeedCloud? Cloud { get; internal set; }

        /// <summary>
        /// Number of minutes that indicates how long a feed can be cached before refreshing from the source.
        /// </summary>
        public TimeSpan Ttl { get; internal set; }

        /// <summary>
        /// Protocol for Web Description Resources (POWDER)
        /// </summary>
        public string? Rating { get; internal set; }

        /// <summary>
        /// Specifies a text input box that can be displayed with the feed.
        /// </summary>
        public FeedTextInput? TextInput { get; internal set; }

        /// <summary>
        /// Identifies the hours of the day during which the feed is not updated.
        /// </summary>
        public IReadOnlyList<int>? SkipHours { get; internal set; }

        /// <summary>
        /// Identifies days of the week during which the feed is not updated.
        /// </summary>
        public WeekDays? SkipDays { get; internal set; }

        /// <summary>
        /// Private list of feed items used when parsing feed.
        /// </summary>
        private List<FeedItem> items = new List<FeedItem>();

        /// <summary>
        /// Read only list with all feed items
        /// </summary>
        public IReadOnlyList<FeedItem> Items => items;

        /// <summary>
        /// Interface implementation for RSS 0.91 feed items
        /// </summary>
        IEnumerable<IRSS_0_91_Item> IRSS_0_91_Feed.Items => Items.Cast<IRSS_0_91_Item>();

        /// <summary>
        /// Interface implementation for RSS 0.92 feed items
        /// </summary>
        IEnumerable<IRSS_0_92_Item> IRSS_0_92_Feed.Items => Items.Cast<IRSS_0_92_Item>();

        /// <summary>
        /// Interface implementation for RSS 1.0 feed items
        /// </summary>
        IEnumerable<IRSS_1_0_Item> IRSS_1_0_Feed.Items => Items.Cast<IRSS_1_0_Item>();

        /// <summary>
        /// Interface implementation for RSS 2.0 feed items
        /// </summary>
        IEnumerable<IRSS_2_0_Item> IRSS_2_0_Feed.Items => Items.Cast<IRSS_2_0_Item>();

        /// <summary>
        /// Interface implementation for Atom feed entries
        /// </summary>
        IEnumerable<IAtomEntry> IAtomFeed.Entries => Items.Cast<IAtomEntry>();

        //iTunes
        public bool HasITunes => ITunes != null;

        public ITunesFeed? ITunes { get; internal set; }

        #region internal

        /// <summary>
        /// Internal property for current parse type. (Root feed level or feed item level)
        /// </summary>
        internal ParseType CurrentParseType => CurrentItem != null ? ParseType.Item : ParseType.Feed;

        /// <summary>
        /// Internal property for the current item being parsed
        /// </summary>
        internal FeedItem? CurrentItem { get; set; }

        /// <summary>
        /// Internal method adding a new feed item to the current feed being parsed.
        /// </summary>
        /// <remarks>
        /// CurrentItem will be set to the new feed item being added.
        /// </remarks>
        /// <returns>The new feed item being added.</returns>
        internal FeedItem AddItem()
        {
            //Create, Save, Set as Current and Return New Item
            CurrentItem = new FeedItem();
            items.Add(CurrentItem);
            return CurrentItem;
        }

        /// <summary>
        /// Internal method closing the current feed item.
        /// </summary>
        internal void CloseItem() => CurrentItem = null;

        #endregion internal
    }
}