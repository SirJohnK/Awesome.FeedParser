using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access Atom feed specified properties
    /// </summary>
    public interface IAtomFeed
    {
        #region Mandatory

        public IEnumerable<IAtomEntry> Entries { get; }

        #endregion Mandatory
    }
}