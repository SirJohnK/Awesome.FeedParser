using System;
using System.Net;

namespace Awesome.FeedParser.Exceptions
{
    /// <summary>
    /// Feed Parser Http Communication Exception.
    /// </summary>
    public class FeedHttpException : Exception
    {
        /// <summary>
        /// Target Url.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Response Http Status Code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Response Http Error Content.
        /// </summary>
        public string? Content { get; set; }
    }
}