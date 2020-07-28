using System.Collections.Generic;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Grouping of item media representations.
    /// </summary>
    /// <remarks>
    ///  Allows grouping of media content that are effectively the same content, yet different representations.
    ///  For instance: the same song recorded in both the WAV and MP3 format. Must only be used for this purpose.
    /// </remarks>
    public class MediaGroup
    {
        /// <summary>
        /// Internal list of media content for parser access
        /// </summary>
        internal List<MediaContent>? content;

        /// <summary>
        /// List of specific media content information.
        /// </summary>
        public IReadOnlyList<MediaContent>? Content => content;

        /// <summary>
        /// Supplemental media information.
        /// </summary>
        public MediaInformation? Information { get; internal set; }
    }
}