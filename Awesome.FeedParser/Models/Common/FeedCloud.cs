namespace Awesome.FeedParser.Models.Common
{
    /// <summary>
    /// Feed cloud publish-subscribe protocol information.
    /// </summary>
    /// <remarks>
    /// Allows processes to register with a cloud to be notified of updates to the feed, implementing a lightweight publish-subscribe protocol for feeds.
    /// </remarks>
    public class FeedCloud
    {
        /// <summary>
        /// Web service domain url.
        /// </summary>
        public string? Domain { get; internal set; }

        /// <summary>
        /// Web service port number.
        /// </summary>
        public string? Port { get; internal set; }

        /// <summary>
        /// Web service domain request path.
        /// </summary>
        public string? Path { get; internal set; }

        /// <summary>
        /// Web service request procedure.
        /// </summary>
        public string? RegisterProcedure { get; internal set; }

        /// <summary>
        /// Web service request notification message.
        /// </summary>
        public string? Protocol { get; internal set; }
    }
}