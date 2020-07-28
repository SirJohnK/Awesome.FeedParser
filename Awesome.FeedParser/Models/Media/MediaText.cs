using System;
using System.Globalization;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Text to be included with the media object.
    /// </summary>
    public class MediaText
    {
        /// <summary>
        /// Time at which the text ends being relevant to the media object.
        /// </summary>
        public TimeSpan? EndTime { get; internal set; }

        /// <summary>
        /// Primary language encapsulated in the media object.
        /// </summary>
        public CultureInfo? Language { get; internal set; }

        /// <summary>
        /// Time at which the text starts being relevant to the media object.
        /// </summary>
        public TimeSpan? StartTime { get; internal set; }

        /// <summary>
        /// The text transcript, closed captioning or lyrics of the media content.
        /// </summary>
        public string? Text { get; internal set; }

        /// <summary>
        /// Specifies the type of text embedded.
        /// </summary>
        /// <remarks>
        /// Possible values are either "plain" or "html".
        /// </remarks>
        public string? Type { get; internal set; }
    }
}