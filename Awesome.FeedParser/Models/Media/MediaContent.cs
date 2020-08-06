using Awesome.FeedParser.Interfaces.Common;
using System;
using System.Globalization;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Feed Media Content information.
    /// </summary>
    public class MediaContent : ICommonItemEnclosure
    {
        /// <summary>
        /// Kilobits per second rate of media.
        /// </summary>
        public int? Bitrate { get; internal set; }

        /// <summary>
        /// Number of audio channels in the media object.
        /// </summary>
        public int? Channels { get; internal set; }

        /// <summary>
        /// Number of seconds the media object plays.
        /// </summary>
        public TimeSpan? Duration { get; internal set; }

        /// <summary>
        /// Determines if the object is a sample or the full version of the object, or even if it is a continuous stream (sample | full | nonstop).
        /// </summary>
        public MediaContentExpression? Expression { get; internal set; }

        /// <summary>
        /// Number of bytes of the media object.
        /// </summary>
        public long? FileSize { get; internal set; }

        /// <summary>
        /// Number of frames per second for the media object.
        /// </summary>
        public int? Framerate { get; internal set; }

        /// <summary>
        /// Height of the media object.
        /// </summary>
        public int? Height { get; internal set; }

        /// <summary>
        /// Supplemental media information.
        /// </summary>
        public MediaInformation? Information { get; internal set; }

        /// <summary>
        /// Determines if this is the default object that should be used for the <media:group>.
        /// </summary>
        public bool? IsDefault { get; internal set; }

        /// <summary>
        /// Primary language encapsulated in the media object. (Language codes: RFC 3066)
        /// </summary>
        public CultureInfo? Language { get; internal set; }

        /// <summary>
        /// IEnclosure interface, feed implementation of Length.
        /// </summary>
        long? ICommonItemEnclosure.Length { get => FileSize; }

        /// <summary>
        /// Type of object (image | audio | video | document | executable).
        /// </summary>
        public MediaContentMedium? Medium { get; internal set; }

        /// <summary>
        /// Number of samples per second taken to create the media object.
        /// </summary>
        public int? Samplingrate { get; internal set; }

        /// <summary>
        /// Standard MIME type of the object.
        /// </summary>
        public string? Type { get; internal set; }

        /// <summary>
        /// Specify the direct URL to the media object.
        /// </summary>
        public Uri? Url { get; internal set; }

        /// <summary>
        /// Width of the media object.
        /// </summary>
        public int? Width { get; internal set; }
    }
}