using Awesome.FeedParser.Models;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access common feed and Atom feed properties.
    /// </summary>
    internal interface ICommonAtomFeed : ICommonAtom
    {
        #region Optional

        /// <summary>
        /// Indicating the program used to generate the feed.
        /// </summary>
        internal FeedGenerator? Generator { get; set; }

        #endregion Optional
    }
}