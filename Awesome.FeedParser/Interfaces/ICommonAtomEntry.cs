using Awesome.FeedParser.Models;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access common feed item and Atom feed entry properties.
    /// </summary>
    internal interface ICommonAtomEntry : ICommonAtom
    {
        #region Optional

        /// <summary>
        /// Conveys a short summary, abstract, or excerpt of the entry.
        /// </summary>
        internal FeedText? Summary { get; set; }

        #endregion Optional
    }
}