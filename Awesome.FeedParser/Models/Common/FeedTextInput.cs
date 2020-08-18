using System;

namespace Awesome.FeedParser.Models.Common
{
    /// <summary>
    /// Specifies a text input box that can be displayed with the feed.
    /// </summary>
    public class FeedTextInput
    {
        /// <summary>
        /// Url to information about text input. (RSS 1.0 Only)
        /// </summary>
        public Uri? About { get; internal set; }

        /// <summary>
        /// Explains the text input area. Maximum length is 500.
        /// </summary>
        public string? Description { get; internal set; }

        /// <summary>
        /// The URL of the CGI script that processes text input requests. Maximum length is 500.
        /// </summary>
        public Uri? Link { get; internal set; }

        /// <summary>
        ///  The name of the text object in the text input area. Maximum length is 20.
        /// </summary>
        public string? Name { get; internal set; }

        /// <summary>
        /// The label of the Submit button in the text input area. Maximum length is 100.
        /// </summary>
        public string? Title { get; internal set; }
    }
}