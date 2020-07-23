using System;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Allows the permissible audience to be declared.
    /// </summary>
    public class MediaRating
    {
        /// <summary>
        /// Media rating.
        /// </summary>
        public string? Rating { get; internal set; }

        /// <summary>
        /// Identifies the rating scheme.
        /// </summary>
        /// <remarks>
        /// Default scheme is urn:simple (adult | nonadult).
        /// </remarks>
        public Uri? Scheme { get; internal set; } = new Uri("urn:simple");
    }
}