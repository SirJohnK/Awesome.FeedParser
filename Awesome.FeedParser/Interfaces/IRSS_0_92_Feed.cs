using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access RSS 0.92 specified properties
    /// </summary>
    public interface IRSS_0_92_Feed : IRSS_0_91_Feed
    {
        #region Mandatory

        public new IEnumerable<IRSS_0_92_Item> Items { get; }

        #endregion Mandatory
    }
}