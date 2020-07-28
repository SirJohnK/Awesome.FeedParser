using System;
using System.Globalization;

namespace Awesome.FeedParser.Interfaces.Media
{
    /// <summary>
    /// Subtitle/CC link.
    /// </summary>
    public interface IMediaSubtitle
    {
        /// <summary>
        /// Subtitle language.
        /// </summary>
        public CultureInfo? Language { get; }

        /// <summary>
        /// Subtitle type.
        /// </summary>
        public string? Type { get; }

        /// <summary>
        /// Link to subtitle.
        /// </summary>
        public Uri? Url { get; }
    }
}