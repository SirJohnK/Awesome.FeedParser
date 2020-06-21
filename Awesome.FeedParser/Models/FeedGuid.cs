using System;

namespace Awesome.FeedParser.Models
{
    public class FeedGuid
    {
        private bool isPermaLink = false;

        public bool IsPermaLink
        {
            get => isPermaLink;
            internal set
            {
                isPermaLink = value;
                Link = isPermaLink && !string.IsNullOrWhiteSpace(Guid) ? new Uri(Guid) : null;
            }
        }

        public string? Guid { get; internal set; }
        public Uri? Link { get; private set; }
    }
}