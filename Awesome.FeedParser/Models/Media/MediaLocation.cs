using System;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Geographical information about various locations captured in the content of a media object.
    /// </summary>
    public class MediaLocation
    {
        /// <summary>
        /// Geographical location.
        /// </summary>
        public GeoCoordinate? Coordinate { get; internal set; }

        /// <summary>
        /// Description of the place whose location is being specified.
        /// </summary>
        public string? Description { get; internal set; }

        /// <summary>
        /// Time at which the reference to a particular location ends in the media object.
        /// </summary>
        public TimeSpan? EndTime { get; internal set; }

        /// <summary>
        /// Time at which the reference to a particular location starts in the media object.
        /// </summary>
        public TimeSpan? StartTime { get; internal set; }
    }
}