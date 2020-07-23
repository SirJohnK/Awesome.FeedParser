using System.Collections.Generic;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Media RSS Parser Feed Item Result Class.
    /// </summary>
    public class MediaItem
    {
        /// <summary>
        /// Internal list of media content for parser access
        /// </summary>
        internal List<MediaContent>? content;

        /// <summary>
        /// List of specific feed item media content information.
        /// </summary>
        public IReadOnlyList<MediaContent>? Content => content;

        /// <summary>
        /// Internal list of media groups for parser access
        /// </summary>
        internal List<MediaGroup>? groups;

        /// <summary>
        /// List of feed item media groups.
        /// </summary>
        public IReadOnlyList<MediaGroup>? Groups => groups;

        /// <summary>
        /// Supplemental feed item media information.
        /// </summary>
        public MediaInformation? Information { get; internal set; }
    }
}