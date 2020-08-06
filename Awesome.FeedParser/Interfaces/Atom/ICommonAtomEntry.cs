using Awesome.FeedParser.Models.Common;
using System;

namespace Awesome.FeedParser.Interfaces.Atom
{
    /// <summary>
    /// Interface to access common feed item and Atom feed entry properties.
    /// </summary>
    internal interface ICommonAtomEntry : ICommonAtom
    {
        #region Optional

        /// <summary>
        /// Contains or links to the complete content of the entry.
        /// </summary>
        internal FeedContent? Content { get; set; }

        /// <summary>
        /// Contains the time of the initial creation or first availability of the entry.
        /// </summary>
        internal DateTime? Published { get; set; }

        /// <summary>
        /// Contains metadata from the source feed if this entry is a copy.
        /// </summary>
        internal FeedLink? Source { get; set; }

        /// <summary>
        /// Conveys a short summary, abstract, or excerpt of the entry.
        /// </summary>
        internal FeedText? Summary { get; set; }

        #endregion Optional
    }
}