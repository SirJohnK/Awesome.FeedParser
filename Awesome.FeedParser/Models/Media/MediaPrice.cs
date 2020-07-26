using System;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Pricing information about a media object.
    /// </summary>
    public class MediaPrice
    {
        /// <summary>
        /// ISO 4217 currency code.
        /// </summary>
        public string? Currency { get; internal set; }

        /// <summary>
        /// Price information.
        /// </summary>
        /// <remarks>
        /// if the type is "package" or "subscription", then info is a URL pointing to package or subscription information.
        /// </remarks>
        public string? Info { get; internal set; }

        /// <summary>
        /// Price of the media object.
        /// </summary>
        public Decimal Price { get; internal set; }

        /// <summary>
        /// Media price type.
        /// </summary>
        public MediaPriceType? Type { get; internal set; }
    }
}