using Awesome.FeedParser.Interfaces.ITunes;
using Awesome.FeedParser.Models.Common;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Awesome.FeedParser.Models.ITunes
{
    /// <summary>
    /// ITunes Parser Feed Result Class.
    /// </summary>
    public class ITunesFeed : ICommonITunes
    {
        /// <summary>
        /// The group responsible for creating the show.
        /// </summary>
        public string? Author { get; internal set; }

        string? ICommonITunes.Author { get => Author; set => Author = value; }

        /// <summary>
        /// The podcast show or hide status. (True/False)
        /// </summary>
        public bool? Block { get; set; }

        bool? ICommonITunes.Block { get => Block; set => Block = value; }

        /// <summary>
        /// The show category information.
        /// </summary>
        public Dictionary<string, List<string>>? Category { get; internal set; }

        /// <summary>
        /// The podcast parental advisory information. Explicit language or adult content. (True/False)
        /// </summary>
        public bool? Explicit { get; internal set; }

        bool? ICommonITunes.Explicit { get => Explicit; set => Explicit = value; }

        /// <summary>
        /// The artwork for the show.
        /// </summary>
        public FeedImage? Image { get; internal set; }

        FeedImage? ICommonITunes.Image { get => Image; set => Image = value; }

        /// <summary>
        /// Internal list of keywords for parser access
        /// </summary>
        internal List<string>? keywords;

        /// <summary>
        /// List of words or phrases used when searching.
        /// </summary>
        public IReadOnlyList<string>? Keywords => keywords;

        List<string>? ICommonITunes.Keywords { get => keywords; set => keywords = value; }

        /// <summary>
        /// The new podcast RSS Feed URL.
        /// </summary>
        public Uri? NewFeedUrl { get; internal set; }

        /// <summary>
        /// The podcast owner contact information. Name and Email address.
        /// </summary>
        public ITunesOwner? Owner { get; internal set; }

        /// <summary>
        /// The show title specific for Apple Podcasts.
        /// </summary>
        public string? Title { get; internal set; }

        string? ICommonITunes.Title { get => Title; set => Title = value; }

        /// <summary>
        /// Used as the title of the podcast.
        /// </summary>
        public string? Subtitle { get; internal set; }

        string? ICommonITunes.Subtitle { get => Subtitle; set => Subtitle = value; }

        /// <summary>
        /// Description of the podcast.
        /// </summary>
        public string? Summary { get; internal set; }

        string? ICommonITunes.Summary { get => Summary; set => Summary = value; }

        /// <summary>
        /// The type of show. Episodic (default) / Serial.
        /// </summary>
        public ITunesType Type { get; internal set; } = ITunesType.Episodic;

        /// <summary>
        /// Podcast country of origin ISO 3166 code.
        /// </summary>
        public RegionInfo? CountryOfOrigin { get; internal set; }
    }
}