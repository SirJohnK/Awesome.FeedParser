using Awesome.FeedParser.Interfaces;
using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Models
{
    public class ITunesItem : ICommonITunes
    {
        public string? Author { get; internal set; }
        string? ICommonITunes.Author { get => Author; set => Author = value; }

        public bool? Block { get; set; }
        bool? ICommonITunes.Block { get => Block; set => Block = value; }

        public TimeSpan? Duration { get; internal set; }

        public bool? Explicit { get; internal set; }
        bool? ICommonITunes.Explicit { get => Explicit; set => Explicit = value; }

        /// <summary>
        /// The episode artwork.
        /// </summary>
        public FeedImage? Image { get; internal set; }

        FeedImage? ICommonITunes.Image { get => Image; set => Image = value; }

        public IEnumerable<string>? Keywords { get; internal set; }
        IEnumerable<string>? ICommonITunes.Keywords { get => Keywords; set => Keywords = value; }

        public DateTime? PubDate { get; internal set; }

        public string? Title { get; internal set; }
        string? ICommonITunes.Title { get => Title; set => Title = value; }

        public string? Subtitle { get; internal set; }
        string? ICommonITunes.Subtitle { get => Subtitle; set => Subtitle = value; }

        public string? Summary { get; internal set; }
        string? ICommonITunes.Summary { get => Summary; set => Summary = value; }

        public int? Season { get; internal set; }

        public int? Episode { get; internal set; }

        public ITunesEpisodeType EpisodeType { get; internal set; } = ITunesEpisodeType.Full;
    }
}