using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class RSSTests
    {
        private const string ResourceId = "RSS_";
        private Dictionary<string, (string name, string file)> resources;

        public RSSTests()
        {
            //Get RSS Resources
            resources = Resources.GetResources(ResourceId);
        }

        [TestMethod]
        public async Task RSS_0_91() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        [TestMethod]
        public async Task RSS_0_92() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        [TestMethod]
        public async Task RSS_1_0() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        [TestMethod]
        public async Task RSS_1_0_Extended() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        [TestMethod]
        public async Task RSS_2_0() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);
    }
}