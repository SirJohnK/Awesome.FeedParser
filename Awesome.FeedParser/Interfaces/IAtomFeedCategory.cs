using System;

namespace Awesome.FeedParser.Interfaces
{
    /// <summary>
    /// Interface to access Atom feed/entry category properties.
    /// </summary>
    public interface IAtomFeedCategory
    {
        #region Required

        /// <summary>
        /// Identifies the category.
        /// </summary>
        public string? Term { get; }

        #endregion Required

        #region Optional

        /// <summary>
        /// A human-readable label for display.
        /// </summary>
        public string? Label { get; }

        /// <summary>
        /// Identifies the categorization scheme via a URI.
        /// </summary>
        public Uri? Scheme { get; }

        #endregion Optional
    }
}