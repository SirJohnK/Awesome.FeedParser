using Awesome.FeedParser.Interfaces;
using System;

namespace Awesome.FeedParser.Models
{
    public class FeedItem : ICommonFeed
    {
        public string? Title { get; set; }

        public Uri? Link { get; set; }

        public string? Description { get; set; }

        public DateTime? PubDate { get; set; }

        //iTunes
        public bool HasITunes => ITunes != null;

        public ITunesItem? ITunes { get; set; }
    }
}