using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access RSS 2.0 specified properties
    /// </summary>
    public interface IRSS_2_0_Feed : IRSS_1_0_Feed
    {
        #region Mandatory

        public new IEnumerable<IRSS_2_0_Item> Items { get; }

        #endregion Mandatory
    }
}