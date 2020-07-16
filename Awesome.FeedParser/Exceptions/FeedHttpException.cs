using System;
using System.Net;

namespace Awesome.FeedParser.Exceptions
{
    /// <summary>
    /// Feed Parser Http Communication Exception.
    /// </summary>
    public class FeedHttpException : Exception
    {
        public string? Url { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public string? Content { get; set; }
    }
}