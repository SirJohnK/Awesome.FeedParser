namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Feed text with text type information.
    /// </summary>
    public class FeedText
    {
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