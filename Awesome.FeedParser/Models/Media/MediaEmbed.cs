using Awesome.FeedParser.Interfaces.Media;
using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Allows inclusion of player-specific embed code for a player to play any video.
    /// </summary>
    public class MediaEmbed : IMediaPlayer
    {
        /// <summary>
        /// Height of embed media object.
        /// </summary>
        public int? Height { get; internal set; }

        /// <summary>
        /// Internal dictionary of parameters for parser access
        /// </summary>
        internal Dictionary<string, string>? parameters;

        /// <summary>
        /// Dictionary of player-specific embed code parameters.
        /// </summary>
        public IReadOnlyDictionary<string, string>? Parameters => parameters;

        /// <summary>
        /// Url to embed media object.
        /// </summary>
        public Uri? Url { get; internal set; }

        /// <summary>
        /// Width of embed media object.
        /// </summary>
        public int? Width { get; internal set; }
    }
}