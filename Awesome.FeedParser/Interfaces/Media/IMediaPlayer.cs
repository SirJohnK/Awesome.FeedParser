using System;

namespace Awesome.FeedParser.Interfaces.Media
{
    /// <summary>
    /// Allows the media object to be accessed through a web browser media player console.
    /// </summary>
    public interface IMediaPlayer
    {
        /// <summary>
        /// Height of the browser window that the URL should be opened in.
        /// </summary>
        public int? Height { get; }

        /// <summary>
        /// URL of the player console that plays the media.
        /// </summary>
        public Uri? Url { get; }

        /// <summary>
        /// Width of the browser window that the URL should be opened in.
        /// </summary>
        public int? Width { get; }
    }
}