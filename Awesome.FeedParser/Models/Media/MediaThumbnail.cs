using Awesome.FeedParser.Interfaces.Media;
using Awesome.FeedParser.Models.Common;
using System;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Image to represent the media.
    /// </summary>
    public class MediaThumbnail : FeedImage, IMediaThumbnail
    {
        /// <summary>
        /// Specifies the time offset in relation to the media object.
        /// </summary>
        public TimeSpan? Time { get; internal set; }
    }
}