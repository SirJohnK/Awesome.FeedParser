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
    public class MediaTests
    {
        [TestMethod]
        public async Task Single_Item_Content()
        {
            //Init
            Feed feed;
            var filename = "Media_Single_Item_Content.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Single_Item_Multiple_Content()
        {
            //Init
            Feed feed;
            var filename = "Media_Single_Item_Multiple_Content.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Single_Item_Group_Multiple_Content()
        {
            //Init
            Feed feed;
            var filename = "Media_Single_Item_Group_Multiple_Content.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Single_Item_Extended()
        {
            //Init
            Feed feed;
            var filename = "Media_Single_Item_Extended.xml";

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