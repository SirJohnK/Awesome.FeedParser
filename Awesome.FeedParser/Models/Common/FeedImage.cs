using System;

namespace Awesome.FeedParser.Models.Common
{
    /// <summary>
    /// Specifies a image that can be displayed with the feed/item.
    /// </summary>
    public class FeedImage
    {
        /// <summary>
        /// Url to information about image. (RSS 1.0 Only)
        /// </summary>
        public Uri? About { get; internal set; }

        /// <summary>
        /// Title/Descriction of the image, it's used in the ALT attribute of the HTML <img> tag when the channel is rendered in HTML.
        /// </summary>
        public string? Title { get; internal set; }

        /// <summary>
        /// Image description.
        /// </summary>
        public string? Description { get; internal set; }

        /// <summary>
        /// Url to the image.
        /// </summary>
        public Uri? Url { get; internal set; }

        /// <summary>
        /// The URL of the site, when the channel is rendered, the image is a link to the site.
        /// </summary>
        public Uri? Link { get; internal set; }

        /// <summary>
        /// The width of the image in pixels.
        /// </summary>
        public int? Width { get; internal set; }

        /// <summary>
        /// The height of the image in pixels.
        /// </summary>
        public int? Height { get; internal set; }
    }
}