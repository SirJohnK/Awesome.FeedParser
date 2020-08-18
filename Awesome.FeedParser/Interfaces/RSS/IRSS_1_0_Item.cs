using System;

namespace Awesome.FeedParser.Interfaces.RSS
{
    /// <summary>
    /// Interface to access RSS 1.0 specified feed items.
    /// </summary>
    public interface IRSS_1_0_Item : IRSS_0_92_Item
    {
        #region Required

        /// <summary>
        /// Url to information about feed item.
        /// </summary>
        public Uri? About { get; }

        #endregion Required
    }
}