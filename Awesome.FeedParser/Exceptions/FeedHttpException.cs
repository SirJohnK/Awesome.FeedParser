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
        /// Initializes a new instance of the FeedHttpException feed parser Http communication exception.
        /// </summary>
        /// <param name="url">Http request Uri/Url.</param>
        /// <param name="statusCode">Http respose status code.</param>
        /// <param name="content">Http response error content.</param>
        public FeedHttpException(Uri? url, HttpStatusCode statusCode, string? content) : base(content)
        {
            //Init
            Url = url;
            StatusCode = statusCode;
            Content = content;
        }

        /// <summary>
        /// Request Http Uri/Url.
        /// </summary>
        public Uri? Url { get; }

        /// <summary>
        /// Response Http Status Code.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Response Http Error Content.
        /// </summary>
        public string? Content { get; }
    }
}