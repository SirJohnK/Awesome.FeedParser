using Awesome.FeedParser.Interfaces.Media;
using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Supplemental media information.
    /// </summary>
    public class MediaInformation
    {
        /// <summary>
        /// Internal list of backLinks for parser access
        /// </summary>
        internal List<Uri>? backLinks;

        /// <summary>
        /// Allows inclusion of all the URLs pointing to a media object.
        /// </summary>
        public IReadOnlyList<Uri>? BackLinks => backLinks;

        /// <summary>
        /// Internal list of categories for parser access
        /// </summary>
        internal List<FeedCategory>? categories;

        /// <summary>
        /// Allows tags or categories to be set for the media.
        /// </summary>
        public IReadOnlyList<IMediaCategory>? Categories => categories;

        /// <summary>
        /// Internal list of credits for parser access
        /// </summary>
        internal List<MediaCredit>? credits;

        /// <summary>
        /// Notable entities and the contributions to the creation of the media object.
        /// </summary>
        public IReadOnlyList<MediaCredit>? Credits => credits;

        /// <summary>
        /// Internal list of comments for parser access
        /// </summary>
        internal List<string>? comments;

        /// <summary>
        /// Allows inclusion of all the comments a media object has received.
        /// </summary>
        public IReadOnlyList<string>? Comments => comments;

        /// <summary>
        /// Allows inclusion of the user perception about a media object in the form of view count, ratings and tags.
        /// </summary>
        public MediaCommunity? Community { get; internal set; }

        /// <summary>
        /// Provides a means to specify the copyright if no other copyright module is used.
        /// </summary>
        public MediaLegal? Copyright { get; internal set; }

        /// <summary>
        /// Description of the media object.
        /// </summary>
        public FeedText? Description { get; internal set; }

        /// <summary>
        /// Allows inclusion of player-specific embed code for a player to play any video.
        /// </summary>
        public MediaEmbed? Embed { get; internal set; }

        /// <summary>
        /// Internal list of hash for parser access
        /// </summary>
        internal List<FeedText>? hash;

        /// <summary>
        /// A "md5" or "sha-1" hash of the media can be used to help verify the integrity and/or look for duplicates.
        /// </summary>
        public IReadOnlyList<IMediaHash>? Hash => hash;

        /// <summary>
        /// Internal list of keywords for parser access
        /// </summary>
        internal List<string>? keywords;

        /// <summary>
        /// A list of words and phrases describing the media content.
        /// </summary>
        public IReadOnlyList<string>? Keywords => keywords;

        /// <summary>
        /// Specify the machine-readable license associated with the content.
        /// </summary>
        public MediaLegal? License { get; internal set; }

        /// <summary>
        /// Internal list of locations for parser access
        /// </summary>
        internal List<MediaLocation>? locations;

        /// <summary>
        /// Geographical information about various locations captured in the content of a media object.
        /// </summary>
        public IReadOnlyList<MediaLocation>? Locations => locations;

        /// <summary>
        /// Media P2P link.
        /// </summary>
        public IMediaPeerLink? PeerLink { get; internal set; }

        /// <summary>
        /// Allows the media object to be accessed through a web browser or media console.
        /// </summary>
        public IMediaPlayer? Player { get; internal set; }

        /// <summary>
        /// Internal list of prices for parser access
        /// </summary>
        internal List<MediaPrice>? prices;

        /// <summary>
        /// Pricing information about a media object.
        /// </summary>
        public IReadOnlyList<MediaPrice>? Prices => prices;

        /// <summary>
        /// Allows the permissible audience to be declared.
        /// </summary>
        public MediaRating? Rating { get; internal set; }

        /// <summary>
        /// Internal list of responses for parser access
        /// </summary>
        internal List<string>? responses;

        /// <summary>
        /// Allows inclusion of a list of all media responses a media object has received.
        /// </summary>
        public IReadOnlyList<string>? Responses => responses;

        /// <summary>
        /// Internal list of restrictions for parser access
        /// </summary>
        internal List<MediaRestriction>? restrictions;

        /// <summary>
        /// Allows inclusion of a list of all media responses a media object has received.
        /// </summary>
        public IReadOnlyList<MediaRestriction>? Restrictions => restrictions;

        /// <summary>
        /// Rights information of a media object.
        /// </summary>
        public MediaRights? Rights { get; internal set; }

        /// <summary>
        /// Specify the status of a media object.
        /// </summary>
        public MediaStatus? Status { get; internal set; }

        /// <summary>
        /// Internal list of scenes for parser access
        /// </summary>
        internal List<MediaScene>? scenes;

        /// <summary>
        /// Specifies various scenes within a media object.
        /// </summary>
        public IReadOnlyList<MediaScene>? Scenes => scenes;

        /// <summary>
        /// Internal list of subtitles for parser access
        /// </summary>
        internal List<FeedLink>? subtitles;

        /// <summary>
        /// Subtitles/CC links.
        /// </summary>
        public IReadOnlyList<IMediaSubtitle>? Subtitles => subtitles;

        /// <summary>
        /// Internal list of texts for parser access
        /// </summary>
        internal List<MediaText>? texts;

        /// <summary>
        /// Allows text to be included with the media object.
        /// </summary>
        public IReadOnlyList<MediaText>? Texts => texts;

        /// <summary>
        /// Internal list of thumbnails for parser access
        /// </summary>
        internal List<MediaThumbnail>? thumbnails;

        /// <summary>
        /// Allows text to be included with the media object.
        /// </summary>
        public IReadOnlyList<IMediaThumbnail>? Thumbnails => thumbnails;

        /// <summary>
        /// The title of the media object.
        /// </summary>
        public FeedText? Title { get; internal set; }
    }
}