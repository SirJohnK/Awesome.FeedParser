using System;

namespace Awesome.FeedParser.Interfaces.Media
{
    /// <summary>
    /// Interface to access Media thumbnails information.
    /// </summary>
    public interface IMediaThumbnail
    {
        /// <summary>
        /// Specifies the height of the thumbnail.
        /// </summary>
        public int? Height { get; }

        /// <summary>
        /// Specifies the time offset in relation to the media object.
        /// </summary>
        public TimeSpan? Time { get; }

        /// <summary>
        /// Specifies the url of the thumbnail.
        /// </summary>
        /// <remarks>
        /// It is a required attribute.
        /// </remarks>
        public Uri? Url { get; }

        /// <summary>
        /// Specifies the width of the thumbnail.
        /// </summary>
        public int? Width { get; }
    }
}