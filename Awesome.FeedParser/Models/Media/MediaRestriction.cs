using System.Collections.Generic;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Allows restrictions to be placed on the aggregator rendering the media in the feed.
    /// </summary>
    /// <remarks>
    /// This element is purely informational and no obligation can be assumed or implied.
    /// </remarks>
    public class MediaRestriction
    {
        /// <summary>
        /// Internal list of entities for parser access
        /// </summary>
        internal List<string>? entities;

        /// <summary>
        /// Restriction entities.
        /// </summary>
        public IReadOnlyList<string>? Entities => entities;

        /// <summary>
        /// Indicates the type of relationship that the restriction represents.
        /// </summary>
        public MediaRestrictionRelationship? Relationship { get; internal set; }

        /// <summary>
        /// Specifies the type of restriction that the media can be syndicated.
        /// </summary>
        public MediaRestrictionType? Type { get; internal set; }
    }
}