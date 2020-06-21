using System;

namespace Awesome.FeedParser.Interfaces
{
    public interface IRSS_0_91_Item
    {
        #region Mandatory

        public string? Title { get; }
        public Uri? Link { get; }
        public string? Description { get; }

        #endregion Mandatory
    }
}