﻿using Awesome.FeedParser.Interfaces.Atom;
using Awesome.FeedParser.Interfaces.Common;
using Awesome.FeedParser.Interfaces.Content;
using Awesome.FeedParser.Interfaces.RSS;
using Awesome.FeedParser.Models.Atom;
using Awesome.FeedParser.Models.Geo;
using Awesome.FeedParser.Models.ITunes;
using Awesome.FeedParser.Models.Media;
using Awesome.FeedParser.Models.Spotify;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;

namespace Awesome.FeedParser.Models.Common
{
    /// <summary>
    /// Main Feed Parser Result Class.
    /// </summary>
    public class Feed : IRSS_0_91_Feed, IRSS_0_92_Feed, IRSS_1_0_Feed, IRSS_2_0_Feed, IAtomFeed, ICommonFeed, ICommonAtomFeed, ICommonContent
    {
        #region Feed Parser

        /// <summary>
        /// Parsed feed type.
        /// </summary>
        public FeedType Type { get; internal set; } = FeedType.Unknown;

        /// <summary>
        /// Parsed feed content types (enum flags)
        /// </summary>
        public FeedContentType ContentType { get; internal set; } = FeedContentType.Basic;

        /// <summary>
        /// ICommon interface, feed implementation of ContentType.
        /// </summary>
        FeedContentType ICommonFeed.ContentType { get => ContentType; set => ContentType = value; }

        /// <summary>
        /// Flag indicating if any parse errors where found while parsing feed.
        /// </summary>
        public bool HasParseErrors => ParseError?.Count > 0;

        /// <summary>
        /// Parse errors found while parsing feed.
        /// </summary>
        public List<ParseError>? ParseError { get; internal set; }

        #endregion Feed Parser

        #region RSS / Atom

        /// <summary>
        /// Url to information about feed. (RSS 1.0 Only)
        /// </summary>
        public Uri? About { get; internal set; }

        /// <summary>
        /// Names one author of the feed. (Atom only)
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
        /// IRSS_2_0_Feed interface, feed item implementation of Categories.
        /// </summary>
        IReadOnlyList<ICommonFeedCategory>? IRSS_2_0_Feed.Categories => Categories;

        /// <summary>
        /// ICommon interface, feed implementation of Categories.
        /// </summary>
        List<FeedCategory>? ICommonFeed.Categories { get => categories; set => categories = value; }

        /// <summary>
        /// IAtomFeed interface, feed item implementation of Categories.
        /// </summary>
        IReadOnlyList<IAtomFeedCategory>? IAtomFeed.Categories => Categories;

        /// <summary>
        /// ICommonAtom interface, feed implementation of Categories.
        /// </summary>
        List<FeedCategory>? ICommonAtom.Categories { get => categories; set => categories = value; }

        /// <summary>
        /// Internal list of contributors for parser access
        /// </summary>
        internal List<FeedPerson>? contributors;

        /// <summary>
        /// Name of one or more contributors to the feed. (Atom only)
        /// </summary>
        public IReadOnlyList<FeedPerson>? Contributors => contributors;

        /// <summary>
        /// ICommonAtom interface, feed implementation of Contributors.
        /// </summary>
        List<FeedPerson>? ICommonAtom.Contributors { get => contributors; set => contributors = value; }

        /// <summary>
        /// Allows processes to register with a cloud to be notified of updates to the feed, implementing a lightweight publish-subscribe protocol for feeds.
        /// </summary>
        public FeedCloud? Cloud { get; internal set; }

        /// <summary>
        /// Contains the complete content of the feed. (Content only)
        /// </summary>
        public FeedContent? Content { get; internal set; }

        /// <summary>
        /// ICommonContent interface, feed item implementation of Content.
        /// </summary>
        FeedContent? ICommonContent.Content { get => Content; set => Content = value; }

        /// <summary>
        /// Copyright notice for content in the feed.
        /// </summary>
        public FeedText? Copyright { get; internal set; }

        /// <summary>
        /// IRSS_0_91_Feed interface, feed implementation of Copyright.
        /// </summary>
        string? IRSS_0_91_Feed.Copyright => Copyright?.Text;

        /// <summary>
        /// Phrase or sentence describing the feed.
        /// </summary>
        public FeedText? Description { get; internal set; }

        /// <summary>
        /// IRSS_0_91_Feed interface, feed implementation of Description.
        /// </summary>
        string? IRSS_0_91_Feed.Description => Description?.Text;

        /// <summary>
        /// ICommon interface, feed implementation of Description.
        /// </summary>
        FeedText? ICommonFeed.Description { get => Description; set => Description = value; }

        /// <summary>
        /// A URL that points to the documentation for the format used for the feed.
        /// </summary>
        public Uri? Docs { get; internal set; }

        /// <summary>
        /// Indicating the program used to generate the feed.
        /// </summary>
        public FeedGenerator? Generator { get; internal set; }

        /// <summary>
        /// IRSS_2_0_Feed interface, feed implementation of Generator.
        /// </summary>
        string? IRSS_2_0_Feed.Generator => Generator?.Generator;

        /// <summary>
        /// ICommonAtom interface, feed implementation of Generator.
        /// </summary>
        FeedGenerator? ICommonAtomFeed.Generator { get => Generator; set => Generator = value; }

        /// <summary>
        /// Identifies a small image which provides iconic visual identification for the feed. (Atom only)
        /// </summary>
        public Uri? Icon { get; internal set; }

        /// <summary>
        /// ICommonAtomFeed interface, feed implementation of Icon.
        /// </summary>
        Uri? ICommonAtomFeed.Icon { get => Icon; set => Icon = value; }

        /// <summary>
        /// Identifies the feed using a universally unique and permanent URI. (Atom only)
        /// </summary>
        public Uri? Id { get; internal set; }

        /// <summary>
        /// ICommonAtom interface, feed implementation of Id.
        /// </summary>
        Uri? ICommonAtom.Id { get => Id; set => Id = value; }

        /// <summary>
        /// Specifies a GIF, JPEG or PNG image that can be displayed with the feed.
        /// </summary>
        public FeedImage? Image { get; internal set; }

        /// <summary>
        /// Private list of feed items used when parsing feed.
        /// </summary>
        private List<FeedItem> items = new List<FeedItem>();

        /// <summary>
        /// Read only list with all feed items.
        /// </summary>
        public IReadOnlyList<FeedItem> Items => items;

        /// <summary>
        /// Interface implementation for RSS 0.91 feed items.
        /// </summary>
        IEnumerable<IRSS_0_91_Item> IRSS_0_91_Feed.Items => Items.Cast<IRSS_0_91_Item>();

        /// <summary>
        /// Interface implementation for RSS 0.92 feed items.
        /// </summary>
        IEnumerable<IRSS_0_92_Item> IRSS_0_92_Feed.Items => Items.Cast<IRSS_0_92_Item>();

        /// <summary>
        /// Interface implementation for RSS 1.0 feed items.
        /// </summary>
        IEnumerable<IRSS_1_0_Item> IRSS_1_0_Feed.Items => Items.Cast<IRSS_1_0_Item>();

        /// <summary>
        /// Interface implementation for RSS 2.0 feed items.
        /// </summary>
        IEnumerable<IRSS_2_0_Item> IRSS_2_0_Feed.Items => Items.Cast<IRSS_2_0_Item>();

        /// <summary>
        /// Interface implementation for Atom feed entries.
        /// </summary>
        IEnumerable<IAtomEntry> IAtomFeed.Entries => Items.Cast<IAtomEntry>();

        /// <summary>
        /// Internal list of items sequence for parser access
        /// </summary>
        internal List<Uri>? itemsSequence;

        /// <summary>
        /// An RDF Sequence is used to contain all the items to denote item order for rendering and reconstruction. (RSS 1.0 Only)
        /// </summary>
        public IReadOnlyList<Uri>? ItemsSequence => itemsSequence;

        /// <summary>
        /// The language the feed is written in. (ISO 639)
        /// </summary>
        public CultureInfo? Language { get; internal set; }

        /// <summary>
        /// The last time the content of the feed changed.
        /// </summary>
        public DateTime? LastBuildDate { get; internal set; }

        /// <summary>
        /// Internal list of links for parser access
        /// </summary>
        internal List<FeedLink>? links;

        /// <summary>
        /// Links to referenced resources (typically a Web page)
        /// </summary>
        public IReadOnlyList<FeedLink>? Links => links;

        /// <summary>
        /// IRSS_0_91_Feed interface, feed implementation of Link.
        /// </summary>
        Uri? IRSS_0_91_Feed.Link => Links?.FirstOrDefault()?.Url;

        /// <summary>
        /// ICommon interface, feed implementation of Link.
        /// </summary>
        List<FeedLink>? ICommonFeed.Links { get => links; set => links = value; }

        /// <summary>
        /// ICommonAtom interface, feed implementation of Link.
        /// </summary>
        List<FeedLink>? ICommonAtom.Links { get => links; set => links = value; }

        /// <summary>
        /// ICommonAtomFeed interface, feed implementation of Logo.
        /// </summary>
        FeedImage? ICommonAtomFeed.Logo { get => Image; set => Image = value; }

        /// <summary>
        /// IAtomFeed interface, feed implementation of Logo.
        /// </summary>
        Uri? IAtomFeed.Logo => Image?.Url;

        /// <summary>
        /// Email address for person responsible for editorial content.
        /// </summary>
        public MailAddress? ManagingEditor { get; internal set; }

        /// <summary>
        /// The publication date for the content in the feed.
        /// </summary>
        public DateTime? PubDate { get; internal set; }

        /// <summary>
        /// ICommon interface, feed implementation of PubDate.
        /// </summary>
        DateTime? ICommonFeed.PubDate { get => PubDate; set => PubDate = value; }

        /// <summary>
        /// Protocol for Web Description Resources (POWDER)
        /// </summary>
        public string? Rating { get; internal set; }

        /// <summary>
        /// IAtomFeed interface, feed entry implementation of Rights.
        /// </summary>
        FeedText? IAtomFeed.Rights => Copyright;

        /// <summary>
        /// ICommonAtom interface, feed entry implementation of Rights.
        /// </summary>
        FeedText? ICommonAtom.Rights { get => Copyright; set => Copyright = value; }

        /// <summary>
        /// Identifies days of the week during which the feed is not updated.
        /// </summary>
        public FeedWeekDays? SkipDays { get; internal set; }

        /// <summary>
        /// Internal list of skip hours for parser access
        /// </summary>
        internal List<int>? skipHours;

        /// <summary>
        /// Identifies the hours of the day during which the feed is not updated.
        /// </summary>
        public IReadOnlyList<int>? SkipHours => skipHours;

        /// <summary>
        /// ICommonAtomFeed interface, feed implementation of Subtitle.
        /// </summary>
        FeedText? ICommonAtomFeed.Subtitle { get => Description; set => Description = value; }

        /// <summary>
        /// IAtomFeed interface, feed implementation of Subtitle.
        /// </summary>
        FeedText? IAtomFeed.Subtitle => Description;

        /// <summary>
        /// Specifies a text input box that can be displayed with the feed.
        /// </summary>
        public FeedTextInput? TextInput { get; internal set; }

        /// <summary>
        /// The name of the feed.
        /// </summary>
        public FeedText? Title { get; internal set; }

        /// <summary>
        /// IRSS_0_91_Feed interface, feed implementation of Title.
        /// </summary>
        string? IRSS_0_91_Feed.Title => Title?.Text;

        /// <summary>
        /// ICommon interface, feed implementation of Title.
        /// </summary>
        FeedText? ICommonFeed.Title { get => Title; set => Title = value; }

        /// <summary>
        /// ICommonAtom interface, feed implementation of Title.
        /// </summary>
        FeedText? ICommonAtom.Title { get => Title; set => Title = value; }

        /// <summary>
        /// Number of minutes that indicates how long a feed can be cached before refreshing from the source.
        /// </summary>
        public TimeSpan? Ttl { get; internal set; }

        /// <summary>
        /// IAtomFeed interface, feed implementation of Updated.
        /// </summary>
        DateTime? IAtomFeed.Updated => LastBuildDate;

        /// <summary>
        /// ICommonAtom interface, feed implementation of Updated.
        /// </summary>
        DateTime? ICommonAtom.Updated { get => LastBuildDate; set => LastBuildDate = value; }

        /// <summary>
        /// Email address for person responsible for technical issues relating to the feed.
        /// </summary>
        public MailAddress? WebMaster { get; internal set; }

        #endregion RSS / Atom

        #region Extended Namespaces

        /// <summary>
        /// Flag indicating if feed has Atom information.
        /// </summary>
        public bool HasAtom => Atom != null;

        /// <summary>
        /// The Atom specific feed information.
        /// </summary>
        public AtomFeed? Atom { get; internal set; }

        /// <summary>
        /// Flag indicating if feed has Content information.
        /// </summary>
        public bool HasContent => Content != null;

        /// <summary>
        /// Flag indicating if feed has Geographical information.
        /// </summary>
        public bool HasGeoInformation => GeoInformation != null;

        /// <summary>
        /// Geographical feed information.
        /// </summary>
        public GeoInformation? GeoInformation { get; internal set; }

        /// <summary>
        /// Flag indicating if feed has Spotify information.
        /// </summary>
        public bool HasSpotify => Spotify != null;

        /// <summary>
        /// The Spotify specific feed information.
        /// </summary>
        public SpotifyFeed? Spotify { get; internal set; }

        /// <summary>
        /// Flag indicating if feed has iTunes information.
        /// </summary>
        public bool HasITunes => ITunes != null;

        /// <summary>
        /// The iTunes specific feed information.
        /// </summary>
        public ITunesFeed? ITunes { get; internal set; }

        /// <summary>
        /// Flag indicating if feed has Media information.
        /// </summary>
        public bool HasMedia => MediaInformation != null;

        /// <summary>
        /// Feed media information.
        /// </summary>
        public MediaInformation? MediaInformation { get; internal set; }

        #endregion Extended Namespaces

        #region internal

        /// <summary>
        /// Internal property for current parse type. (Root feed level or feed item level)
        /// </summary>
        internal ParseType CurrentParseType => CurrentItem != null ? ParseType.Item : ParseType.Feed;

        /// <summary>
        /// Internal property for the current item being parsed
        /// </summary>
        internal FeedItem? CurrentItem { get; private set; }

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