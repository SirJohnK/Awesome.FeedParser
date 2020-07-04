using Awesome.FeedParser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access RSS 0.91 specified properties.
    /// </summary>
    public interface IRSS_0_91_Feed
    {
        #region Required

        /// <summary>
        /// Phrase or sentence describing the feed.
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// The language the feed is written in. (ISO 639)
        /// </summary>
        public CultureInfo? Language { get; }

        /// <summary>
        /// The URL to the HTML website corresponding to the feed.
        /// </summary>
        public Uri? Link { get; }

        /// <summary>
        /// The name of the feed.
        /// </summary>
        public string? Title { get; }

        #endregion Required

        #region Optional

        /// <summary>
        /// Copyright notice for content in the feed.
        /// </summary>
        public string? Copyright { get; }

        /// <summary>
        /// A URL that points to the documentation for the format used for the feed.
        /// </summary>
        public Uri? Docs { get; }

        /// <summary>
        /// Specifies a GIF, JPEG or PNG image that can be displayed with the feed.
        /// </summary>
        public FeedImage? Image { get; }

        /// <summary>
        /// RSS 0.91 feed items
        /// </summary>
        IEnumerable<IRSS_0_91_Item> Items { get; }

        /// <summary>
        /// The last time the content of the feed changed.
        /// </summary>
        public DateTime? LastBuildDate { get; }

        /// <summary>
        /// Email address for person responsible for editorial content.
        /// </summary>
        public MailAddress? ManagingEditor { get; }

        /// <summary>
        /// The publication date for the content in the feed.
        /// </summary>
        public DateTime? PubDate { get; }

        /// <summary>
        /// Protocol for Web Description Resources (POWDER)
        /// </summary>
        public string? Rating { get; }

        /// <summary>
        /// Identifies days of the week during which the feed is not updated.
        /// </summary>
        public WeekDays? SkipDays { get; }

        /// <summary>
        /// Identifies the hours of the day during which the feed is not updated.
        /// </summary>
        public IReadOnlyList<int>? SkipHours { get; }

        /// <summary>
        /// Specifies a text input box that can be displayed with the feed.
        /// </summary>
        public FeedTextInput? TextInput { get; }

        /// <summary>
        /// Email address for person responsible for technical issues relating to the feed.
        /// </summary>
        public MailAddress? WebMaster { get; }

        #endregion Optional
    }
}