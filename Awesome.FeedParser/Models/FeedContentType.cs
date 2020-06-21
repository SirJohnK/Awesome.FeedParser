using System;

namespace Awesome.FeedParser.Models
{
    [Flags]
    public enum FeedContentType
    {
        Basic = 0,
        Content = 1,
        ITunes = 2,
        MediaRSS = 4,
        Spotify = 8,
        Youtube = 16,
    }
}