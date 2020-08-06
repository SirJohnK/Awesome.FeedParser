using Awesome.FeedParser.Models.Common;
using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces.ITunes
{
    /// <summary>
    /// Interface to access common feed/item ITunes properties.
    /// </summary>
    internal interface ICommonITunes
    {
        /// <summary>
        /// The group responsible for creating the show/episode.
        /// </summary>
        internal string? Author { get; set; }

        /// <summary>
        /// The podcast/episode show or hide status. (True/False)
        /// </summary>
        internal bool? Block { get; set; }

        /// <summary>
        /// The podcast/episode parental advisory information. Explicit language or adult content. (True/False)
        /// </summary>
        internal bool? Explicit { get; set; }

        /// <summary>
        /// The artwork for the show/episode.
        /// </summary>
        internal FeedImage? Image { get; set; }

        /// <summary>
        /// List of words or phrases used when searching.
        /// </summary>
        internal IEnumerable<string>? Keywords { get; set; }

        /// <summary>
        /// The show/episode title specific for Apple Podcasts.
        /// </summary>
        internal string? Title { get; set; }

        /// <summary>
        /// Used as the title of the podcast/episode.
        /// </summary>
        internal string? Subtitle { get; set; }

        /// <summary>
        /// Description of the podcast/episode.
        /// </summary>
        internal string? Summary { get; set; }
    }
}