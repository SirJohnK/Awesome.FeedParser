using System.Net.Mail;

namespace Awesome.FeedParser.Models.ITunes
{
    /// <summary>
    /// The podcast owner contact information.
    /// </summary>
    public class ITunesOwner
    {
        /// <summary>
        /// Name of the owner.
        /// </summary>
        public string? Name { get; internal set; }

        /// <summary>
        /// Email address of the owner.
        /// </summary>
        public MailAddress? Email { get; internal set; }
    }
}