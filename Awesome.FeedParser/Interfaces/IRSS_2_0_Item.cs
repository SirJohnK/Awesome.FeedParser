using Awesome.FeedParser.Models;
using System;
using System.Net.Mail;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access RSS 2.0 specified feed items.
    /// </summary>
    public interface IRSS_2_0_Item : IRSS_1_0_Item
    {
        #region Optional

        /// <summary>
        /// Email address of the author of the feed item.
        /// </summary>
        public MailAddress? Author { get; }

        /// <summary>
        /// URL of a page for comments relating to the feed item.
        /// </summary>
        public Uri? Comments { get; }

        /// <summary>
        /// A string/link that uniquely identifies the feed item.
        /// </summary>
        public FeedGuid? Guid { get; }

        /// <summary>
        /// The publication date for the content in the feed item.
        /// </summary>
        public DateTime? PubDate { get; }

        #endregion Optional
    }
}