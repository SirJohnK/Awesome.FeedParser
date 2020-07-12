using System;
using System.Net.Mail;

namespace Awesome.FeedParser.Models
{
    /// <summary>
    /// Feed Person information.
    /// </summary>
    public class FeedPerson
    {
        /// <summary>
        ///  A human-readable name for the person.
        /// </summary>
        public string? Name { get; internal set; }

        /// <summary>
        /// Contains a home page for the person.
        /// </summary>
        public Uri? Uri { get; internal set; }

        /// <summary>
        /// Contains an email address for the person.
        /// </summary>
        public MailAddress? Email { get; internal set; }
    }
}