using System.Collections.Generic;
using System.Globalization;

namespace Awesome.FeedParser.Models.Spotify
{
    /// <summary>
    /// Spotify Parser Feed Result Class.
    /// </summary>
    public class SpotifyFeed
    {
        /// <summary>
        /// Number of concurrent episodes (items) to display.
        /// </summary>
        public int? Limit { get; internal set; }

        /// <summary>
        /// Internal list of country of origin for parser access
        /// </summary>
        internal List<RegionInfo>? countryOfOrigin;

        /// <summary>
        /// Defines the intended market/territory ranked in order of priority where the podcast is relevant to the consumer. (List of ISO 3166 codes).
        /// </summary>
        public IReadOnlyList<RegionInfo>? CountryOfOrigin => countryOfOrigin;
    }
}