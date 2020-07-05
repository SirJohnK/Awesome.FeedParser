using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Awesome.FeedParser.Utils
{
    internal static class StringExtensions
    {
        public static MailAddress ToMailAddress(this string address)
        {
            //Attempt to parse address
            var match = Regex.Match(address, @"^(?<address>.+@.+)\s\((?<name>.+)\)$");
            if (match.Success)
                return new MailAddress(match.Groups["address"].Value.Trim(), match.Groups["name"].Value.Trim());
            else
                return new MailAddress(address);
        }
    }
}