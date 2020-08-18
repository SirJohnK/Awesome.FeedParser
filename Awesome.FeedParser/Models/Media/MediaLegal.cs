using Awesome.FeedParser.Models.Common;
using System;

namespace Awesome.FeedParser.Models.Media
{
    /// <summary>
    /// Media legal information.
    /// </summary>
    public class MediaLegal : FeedText
    {
        /// <summary>
        /// URL for a terms of use page, additional copyright or license information.
        /// </summary>
        public Uri? Url { get; internal set; }
    }
}