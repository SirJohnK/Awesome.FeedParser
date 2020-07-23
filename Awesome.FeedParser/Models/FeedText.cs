using Awesome.FeedParser.Interfaces.Media;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Feed text with text type information.
    /// </summary>
    public class FeedText : IMediaHash
    {
        /// <summary>
        /// IMediaHash interface, feed text implementation of Algo.
        /// </summary>
        string? IMediaHash.Algo => Type;

        /// <summary>
        /// IMediaHash interface, feed text implementation of Hash.
        /// </summary>
        string? IMediaHash.Hash => Text;

        /// <summary>
        /// Text content.
        /// </summary>
        public string? Text { get; internal set; }

        /// <summary>
        /// Text content type. Usually text, html or xhtml. (Mime type)
        /// </summary>
        public string? Type { get; internal set; }
    }
}