using System.Collections.Generic;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Community related content.
    /// </summary>
    /// <remarks>
    /// Allows inclusion of the user perception about a media object in the form of view count, ratings and tags.
    /// </remarks>
    public class MediaCommunity
    {
        #region Star Rating

        /// <summary>
        /// Rating average.
        /// </summary>
        public double? RatingAverage { get; internal set; }

        /// <summary>
        /// Number of ratings.
        /// </summary>
        public int? RatingCount { get; internal set; }

        /// <summary>
        /// Minimum rating.
        /// </summary>
        public int? RatingMax { get; internal set; }

        /// <summary>
        /// Maximum rating.
        /// </summary>
        public int? RatingMin { get; internal set; }

        #endregion Star Rating

        #region Statistics

        /// <summary>
        /// View count.
        /// </summary>
        public int? Views { get; internal set; }

        /// <summary>
        /// Favorite count.
        /// </summary>
        public int? Favorites { get; internal set; }

        #endregion Statistics

        #region Tags

        /// <summary>
        /// Internal list of tags for parser access
        /// </summary>
        internal List<(string Tag, int Weight)>? tags;

        /// <summary>
        /// List of user-generated tags.
        /// </summary>
        public IReadOnlyList<(string Tag, int Weight)>? Tags => tags;

        #endregion Tags
    }
}