namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Feed link type.
    /// </summary>
    public enum FeedLinkType
    {
        /// <summary>
        /// Unknown link type.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// An alternate representation of the entry or feed, for example a permalink to the html version of the entry, or the front page of the weblog.
        /// </summary>
        Alternate = 1,

        /// <summary>
        /// A related resource which is potentially large in size and might require special handling, for example an audio or video recording.
        /// </summary>
        Enclosure = 2,

        /// <summary>
        /// An document related to the entry or feed.
        /// </summary>
        Related = 3,

        /// <summary>
        /// The feed/entry itself.
        /// </summary>
        Self = 4,

        /// <summary>
        /// The source of the information provided in the entry.
        /// </summary>
        Via = 5,
    }
}