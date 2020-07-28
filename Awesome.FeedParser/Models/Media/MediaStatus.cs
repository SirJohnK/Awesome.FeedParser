namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Specify the status of a media object
    /// </summary>
    public class MediaStatus
    {
        /// <summary>
        /// Explaining why a media object has been blocked/deleted.
        /// </summary>
        /// <remarks>
        /// It can be plain text or a URL.
        /// </remarks>
        public string? Reason { get; internal set; }

        /// <summary>
        /// Status State.
        /// </summary>
        /// <remarks>
        /// "active" means a media object is active in the system.
        /// "blocked" means a media object is blocked by the publisher.
        /// "deleted" means a media object has been deleted by the publisher.
        /// </remarks>
        public MediaStatusState? State { get; internal set; }
    }
}