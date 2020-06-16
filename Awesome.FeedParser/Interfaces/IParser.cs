using Awesome.FeedParser.Models;
using System.Threading.Tasks;
using System.Xml;

namespace Awesome.FeedParser.Interfaces
{
    public interface IParser
    {
        public Task<bool> Parse(XmlReader reader, Feed feed);
    }
}