using System;

namespace Awesome.FeedParser.Interfaces.Media
{
    /// <summary>
    /// Interface to access Media feed/item category properties.
    /// </summary>
    public interface IMediaCategory
    {
        #region Required

        /// <summary>
        /// Identifies the category.
        /// </summary>
        public string? Category { get; }

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