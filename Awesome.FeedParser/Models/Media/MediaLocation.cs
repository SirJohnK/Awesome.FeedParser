﻿using Awesome.FeedParser.Models.Geo;
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
        /// <remarks>
        /// Init to empty GeoInformation.
        /// </remarks>
        public GeoInformation GeoInformation { get; internal set; } = new GeoInformation();

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