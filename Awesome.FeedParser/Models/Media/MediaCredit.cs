using System;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Media credit information.
    /// </summary>
    /// <remarks>
    /// Notable entity and the contribution to the creation of the media object.
    /// </remarks>
    public class MediaCredit
    {
        /// <summary>
        /// Entity name.
        /// </summary>
        public string? Name { get; internal set; }

        /// <summary>
        /// Specifies the role the entity played.
        /// </summary>
        /// <remarks>
        /// Must be lowercase.
        /// </remarks>
        public string? Role { get; internal set; }

        /// <summary>
        /// URI that identifies the role scheme.
        /// </summary>
        /// <remarks>
        /// Possible values for this attribute are ( urn:ebu | urn:yvs ).
        /// </remarks>
        public Uri? Scheme { get; internal set; }
    }
}