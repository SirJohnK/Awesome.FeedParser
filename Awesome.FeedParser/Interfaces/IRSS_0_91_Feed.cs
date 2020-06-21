using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access RSS 0.91 specified properties
    /// </summary>
    public interface IRSS_0_91_Feed
    {
        #region Mandatory

        public string? Title { get; }
        public Uri? Link { get; }
        public string? Description { get; }
        public IEnumerable<IRSS_0_91_Item> Items { get; }

        #endregion Mandatory
    }
}