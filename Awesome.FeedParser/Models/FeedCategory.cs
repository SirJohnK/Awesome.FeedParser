using Awesome.FeedParser.Interfaces;
using System;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Feed or Item category information.
    /// </summary>
    public class FeedCategory : ICommonFeedCategory, IAtomFeedCategory
    {
        /// <summary>
        /// Identifies a hierarchic location in the indicated taxonomy.
        /// </summary>
        public string? Category { get; internal set; }

        /// <summary>
        /// Identifies a categorization taxonomy.
        /// </summary>
        public string? Domain { get; internal set; }

        /// <summary>
        /// A human-readable label for display.
        /// </summary>
        public string? Label { get; internal set; }

        /// <summary>
        /// Identifies the category.
        /// </summary>
        string? IAtomFeedCategory.Term => Category;

        /// <summary>
        /// Identifies the categorization scheme via a URI.
        /// </summary>
        public Uri? Scheme { get; internal set; }
    }
}