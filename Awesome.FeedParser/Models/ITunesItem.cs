using Awesome.FeedParser.Interfaces;
using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// ITunes Parser Feed Item Result Class.
    /// </summary>
    public class ITunesItem : ICommonITunes
    {
        /// <summary>
        ///  The group responsible for creating the episode.
        /// </summary>
        public string? Author { get; internal set; }

        string? ICommonITunes.Author { get => Author; set => Author = value; }

        /// <summary>
        /// The episode show or hide status. (True/False)
        /// </summary>
        public bool? Block { get; set; }

        bool? ICommonITunes.Block { get => Block; set => Block = value; }

        /// <summary>
        /// The duration of an episode.
        /// </summary>
        public TimeSpan? Duration { get; internal set; }

        /// <summary>
        /// The episode parental advisory information. Explicit language or adult content. (True/False)
        /// </summary>
        public bool? Explicit { get; internal set; }

        bool? ICommonITunes.Explicit { get => Explicit; set => Explicit = value; }

        /// <summary>
        /// The episode artwork.
        /// </summary>
        public FeedImage? Image { get; internal set; }

        FeedImage? ICommonITunes.Image { get => Image; set => Image = value; }

        /// <summary>
        /// List of words or phrases used when searching.
        /// </summary>
        public IEnumerable<string>? Keywords { get; internal set; }

        IEnumerable<string>? ICommonITunes.Keywords { get => Keywords; set => Keywords = value; }

        /// <summary>
        /// An episode title specific for Apple Podcasts.
        /// </summary>
        public string? Title { get; internal set; }

        string? ICommonITunes.Title { get => Title; set => Title = value; }

        /// <summary>
        /// Used as the title of the episode.
        /// </summary>
        public string? Subtitle { get; internal set; }

        string? ICommonITunes.Subtitle { get => Subtitle; set => Subtitle = value; }

        /// <summary>
        /// Description of the episode.
        /// </summary>
        public string? Summary { get; internal set; }

        string? ICommonITunes.Summary { get => Summary; set => Summary = value; }

        /// <summary>
        /// The episode season number.
        /// </summary>
        public int? Season { get; internal set; }

        /// <summary>
        /// An episode number.
        /// </summary>
        public int? Episode { get; internal set; }

        /// <summary>
        /// The episode type. (Full, Trailer or Bonus)
        /// </summary>
        public ITunesEpisodeType EpisodeType { get; internal set; } = ITunesEpisodeType.Full;
    }
}