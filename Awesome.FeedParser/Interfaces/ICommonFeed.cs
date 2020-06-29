using Awesome.FeedParser.Models;
using System;
using System.Collections.Generic;

namespace Awesome.FeedParser.Interfaces
{
    internal interface ICommonFeed
    {
        internal FeedContentType ContentType { get; set; }
        internal string? Title { get; set; }
        internal string? Description { get; set; }
        internal Uri? Link { get; set; }
        internal DateTime? PubDate { get; set; }
        internal List<FeedCategory>? Categories { get; set; }
    }
}