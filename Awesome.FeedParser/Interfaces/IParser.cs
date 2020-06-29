using Awesome.FeedParser.Models;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Interfaces
{
    internal interface IParser
    {
        Task<bool> Parse(XmlReader reader, Feed feed);
    }
}