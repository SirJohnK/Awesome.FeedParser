using Awesome.FeedParser;
using Awesome.FeedParser.Models.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            //Init
            var feed = await FeedParser.ParseFeedAsync(@"https://feed.khz.se/nordigt", CancellationToken.None);

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task TestMethod2()
        {
            //Init
            Feed feed;
            var filename = "nordigt.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task RSS_0_91_Test()
        {
            //Init
            Feed feed;
            var filename = "RSS_0_91.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task RSS_0_92_Test()
        {
            //Init
            Feed feed;
            var filename = "RSS_0_92.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task RSS_1_0_Test()
        {
            //Init
            Feed feed;
            var filename = "RSS_1_0.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task RSS_1_0_Extended_Test()
        {
            //Init
            Feed feed;
            var filename = "RSS_1_0_Extended.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task RSS_2_0_Test()
        {
            //Init
            Feed feed;
            var filename = "RSS_2_0.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Atom_Test()
        {
            //Init
            Feed feed;
            var filename = "Atom.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Atom_Extended_Test()
        {
            //Init
            Feed feed;
            var filename = "Atom_Extended.xml";

            //Open feed file
            using (var stream = File.OpenRead($"C:\\Testlab\\Feeds\\{filename}"))
            {
                feed = await FeedParser.ParseFeedAsync(filename, stream, CancellationToken.None);
            }

            //Assert
            feed.Should().NotBeNull();
        }

        [TestMethod]
        public async Task Atom_Extended_II_Test()
        {
            //Init
            Feed feed;
            var filename = "Atom_Extended_II.xml";

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