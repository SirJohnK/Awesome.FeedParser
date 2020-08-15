using Awesome.FeedParser;
using Awesome.FeedParser.Models.Common;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Helpers
{
    internal static class TestHelpers
    {
        internal static async Task ParseResource(string name, Dictionary<string, (string name, string file)> resources)
        {
            //Init
            var source = resources[name];
            var result = resources.ContainsKey($"{name}_Result") ? resources[$"{name}_Result"] : (name: string.Empty, file: $"{name}_Result.json");
            var serializer = new JsonSerializer() { ContractResolver = new JsonCustomResolver() };

            //Open feed resource
            using Stream stream = Resources.GetResource(source.name);

            //Parse feed
            Feed feed = await FeedParser.ParseFeedAsync(source.file, stream, CancellationToken.None);

#if UPDATERESULT
            //Update Result Resource
            using StreamWriter file = File.CreateText(Path.Combine(Resources.Location, result.file));
            serializer.Serialize(file, feed);

            //Assert
            feed.Should().NotBeNull();
#else
            //Get Result for feed verification
            using StreamReader resultStream = new StreamReader(Resources.GetResource(result.name));
            var matchFeed = (Feed)serializer.Deserialize(resultStream, typeof(Feed));

            //Assert
            matchFeed.Should().BeEquivalentTo(feed);
#endif
        }
    }
}