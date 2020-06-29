using System.Globalization;

namespace Awesome.FeedParser.Models
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
        /// Podcast country of origin ISO 3166 code.
        /// </summary>
        public RegionInfo? CountryOfOrigin { get; internal set; }
    }
}