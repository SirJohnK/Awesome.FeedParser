using System;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Contains information about a particular scene.
    /// </summary>
    public class MediaScene
    {
        /// <summary>
        /// Title of the scene.
        /// </summary>
        public string? Title { get; internal set; }

        /// <summary>
        /// Description of the scene.
        /// </summary>
        public string? Description { get; internal set; }

        /// <summary>
        /// Time at which the scene ends in the media object.
        /// </summary>
        public TimeSpan? EndTime { get; internal set; }

        /// <summary>
        /// Time at which the scene starts in the media object.
        /// </summary>
        public TimeSpan? StartTime { get; internal set; }
    }
}