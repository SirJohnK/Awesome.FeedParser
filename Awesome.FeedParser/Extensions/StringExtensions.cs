using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Awesome.FeedParser.Extensions
{
    /// <summary>
    /// String extension methods for feed parsing.
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// Parse address string to mail address with display name.
        /// </summary>
        /// <param name="address">Mail address text.</param>
        /// <returns>MailAddress with parsed input address text.</returns>
        internal static MailAddress ToMailAddress(this string address)
        {
            //Attempt to parse address
            var match = Regex.Match(address, @"^(?<address>.+@.+)\s\((?<name>.+)\)$");
            if (match.Success)
                return new MailAddress(match.Groups["address"].Value.Trim(), match.Groups["name"].Value.Trim());
            else
                return new MailAddress(address);
        }

        /// <summary>
        /// Split camel case text into word delimited text.
        /// </summary>
        /// <param name="camelCaseText">Camel case text.</param>
        /// <param name="delimiter">Word delimiter. (Default: space)</param>
        /// <returns>Word delimited text.</returns>
        internal static string SplitCamelCase(this string camelCaseText, string delimiter = " ")
        {
            //Split Camel Case and return result
            return string.Join(delimiter, Regex.Split(camelCaseText, @"(?<!^)(?=[A-Z])")); ;
        }
    }
}