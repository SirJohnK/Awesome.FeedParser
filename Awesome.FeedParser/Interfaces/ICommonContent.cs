using Awesome.FeedParser.Models;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access common feed/item content properties.
    /// </summary>
    internal interface ICommonContent
    {
        /// <summary>
        /// Contains the complete content of the feed/item.
        /// </summary>
        internal FeedContent? Content { get; set; }
    }
}