﻿using System;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Feed content information.
    /// </summary>
    public class FeedContent : FeedText
    {
        /// <summary>
        /// Feed content source URL.
        /// </summary>
        public Uri? Source { get; internal set; }
    }
}