using Awesome.FeedParser.Models.Common;

namespace Awesome.FeedParser.Interfaces.Content
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