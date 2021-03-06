﻿using Awesome.FeedParser.Interfaces.Atom;
using Awesome.FeedParser.Interfaces.Media;
using System;
using System.Globalization;

namespace Awesome.FeedParser.Models.Common
{
    /// <summary>
    /// Feed link information.
    /// </summary>
    public class FeedLink : IAtomEntrySource, IMediaPeerLink, IMediaSubtitle
    {
        /// <summary>
        /// IAtomEntrySource interface, feed link implementation of Id.
        /// </summary>
        Uri? IAtomEntrySource.Id => Url;

        /// <summary>
        /// Indicates the language of the referenced resource.
        /// </summary>
        public CultureInfo? Language { get; internal set; }

        /// <summary>
        /// The length of the resource, in bytes.
        /// </summary>
        public long? Length { get; internal set; }

        /// <summary>
        /// Indicates the media type of the resource.
        /// </summary>
        public string? MediaType { get; internal set; }

        /// <summary>
        /// Human readable information about the link, typically for display purposes.
        /// </summary>
        public string? Text { get; internal set; }

        /// <summary>
        /// IAtomEntrySource interface, feed link implementation of Title.
        /// </summary>
        string? IAtomEntrySource.Title => Text;

        /// <summary>
        /// Feed link relationship type.
        /// </summary>
        public FeedLinkType Type { get; internal set; }

        /// <summary>
        /// IMediaPeerLink interface, feed link implementation of Type.
        /// </summary>
        string? IMediaPeerLink.Type => MediaType;

        /// <summary>
        /// IMediaSubtitle interface, feed link implementation of Type.
        /// </summary>
        string? IMediaSubtitle.Type => MediaType;

        /// <summary>
        /// Indicates the last time the referenced resource was modified in a significant way.
        /// </summary>
        public DateTime? Updated { get; internal set; }

        /// <summary>
        /// The URI of the referenced resource.
        /// </summary>
        public Uri? Url { get; internal set; }
    }
}