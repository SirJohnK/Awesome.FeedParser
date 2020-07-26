using System.Collections.Generic;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Geographical information.
    /// </summary>
    /// <remarks>
    /// Used by Geo RSS parser.
    /// </remarks>
    public class GeoInformation
    {
        /// <summary>
        /// Internal list of coordinates for parser access
        /// </summary>
        internal List<GeoCoordinate>? coordinates;

        /// <summary>
        /// List of geographical coordinates.
        /// </summary>
        public IReadOnlyList<GeoCoordinate>? Coordinates => coordinates;

        /// <summary>
        /// Contain GPS elevation reading, i.e. height in meters.
        /// </summary>
        public int? Elevation { get; internal set; }

        /// <summary>
        /// Feauture name of the geographical information.
        /// </summary>
        public string? FeatureName { get; internal set; }

        /// <summary>
        /// Feauture type of the geographical information.
        /// </summary>
        /// <example>city</example>
        public string? FeatureType { get; internal set; }

        /// <summary>
        /// Contain the floor number of a building.
        /// </summary>
        public int? Floor { get; internal set; }

        /// <summary>
        /// Relationship of the geographical information.
        /// </summary>
        /// <example>is-centered-at</example>
        public string? Relationship { get; internal set; }

        /// <summary>
        /// Type of the geographical information.
        /// </summary>
        public GeoType? Type { get; internal set; }
    }
}