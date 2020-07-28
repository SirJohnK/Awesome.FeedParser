using Awesome.FeedParser;
using Awesome.FeedParser.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class GeoRSSTests
    {
        [TestMethod]
        public async Task SIMPLE_Single_Item_Point()
        {
            //Init
            Feed feed;
            var filename = "GeoRSS_SIMPLE_Single_Item_Point.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GML_Single_Item_Point()
        {
            //Init
            Feed feed;
            var filename = "GeoRSS_GML_Single_Item_Point.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GML_Feed_Line_Multiple_Item_Point()
        {
            //Init
            Feed feed;
            var filename = "GeoRSS_GML_Feed_Line_Multiple_Item_Point.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }
    }
}