using System;

namespace Awesome.FeedParser.Interfaces.Media
{
    /// <summary>
    /// Interface to access Media P2P link.
    /// </summary>
    public interface IMediaPeerLink
    {
        /// <summary>
        /// Indicates the media type of the P2P link.
        /// </summary>
        public string? Type { get; }

        /// <summary>
        /// The URI of the P2P link.
        /// </summary>
        public Uri? Url { get; }
    }
}