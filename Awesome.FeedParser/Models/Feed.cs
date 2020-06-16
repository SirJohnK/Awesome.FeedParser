using Awesome.FeedParser.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Main Feed Parser Result Class.
    /// </summary>
    public class Feed : ICommonFeed
    {
        public FeedType Type { get; set; } = FeedType.Unknown;

        public string? Title { get; set; }

        public Uri? Link { get; set; }

        public string? Description { get; set; }

        public FeedImage? Image { get; set; }

        public DateTime? PubDate { get; set; }

        public CultureInfo? Language { get; set; }

        public string? Copyright { get; set; }

        private List<FeedItem> items = new List<FeedItem>();
        public ReadOnlyCollection<FeedItem> Items => items.AsReadOnly();

        //iTunes
        public bool HasITunes => ITunes != null;

        public ITunesFeed? ITunes { get; set; }

        #region internal

        internal FeedItem? CurrentItem { get; private set; }

        internal FeedItem AddItem()
        {
            //Create, Save, Set as Current and Return New Item
            CurrentItem = new FeedItem();
            items.Add(CurrentItem);
            return CurrentItem;
        }

        internal void CloseItem() => CurrentItem = null;

        #endregion internal
    }
}