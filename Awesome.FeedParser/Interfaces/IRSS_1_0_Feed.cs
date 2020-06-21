using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access RSS 1.0 specified properties
    /// </summary>
    public interface IRSS_1_0_Feed : IRSS_0_92_Feed
    {
        #region Mandatory

        public string? About { get; }

        public new IEnumerable<IRSS_1_0_Item> Items { get; }

        #endregion Mandatory
    }
}