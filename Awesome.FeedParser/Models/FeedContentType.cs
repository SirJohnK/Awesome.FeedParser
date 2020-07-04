using System;

namespace Awesome.FeedParser.Models
{
    [Flags]
    public enum FeedContentType
    {
        Basic = 0,
        Rdf = 1,
        Content = 2,
        ITunes = 4,
        MediaRSS = 8,
        Spotify = 16,
        Youtube = 32,
    }
}