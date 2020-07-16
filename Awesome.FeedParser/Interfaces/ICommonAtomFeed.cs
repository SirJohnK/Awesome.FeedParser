using Awesome.FeedParser.Models;
using System;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access common feed and Atom feed properties.
    /// </summary>
    internal interface ICommonAtomFeed : ICommonAtom
    {
        #region Optional

        /// <summary>
        /// Identifies a small image which provides iconic visual identification for the feed.
        /// </summary>
        internal Uri? Icon { get; set; }

        /// <summary>
        /// Indicating the program used to generate the feed.
        /// </summary>
        internal FeedGenerator? Generator { get; set; }

        /// <summary>
        /// Identifies a larger image which provides visual identification for the feed.
        /// </summary>
        internal FeedImage? Logo { get; set; }

        /// <summary>
        /// Contains a human-readable description or subtitle for the feed.
        /// </summary>
        internal FeedText? Subtitle { get; set; }

        #endregion Optional
    }
}