using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Helpers;

namespace Tests
{
    /// <summary>
    /// Tests for different Media RSS feeds.
    /// </summary>
    [TestClass]
    public class MediaTests
    {
        private const string ResourceId = "Media_";
        private Dictionary<string, (string name, string file)> resources;

        /// <summary>
        /// Setup and get Media RSS resources.
        /// </summary>
        public MediaTests()
        {
            //Get Media Resources
            resources = Resources.GetResources(ResourceId);
        }

        /// <summary>
        /// Parse and verify single item content Media feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task Media_Single_Item_Content() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        /// <summary>
        /// Parse and verify single item multiple content Media feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task Media_Single_Item_Multiple_Content() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        /// <summary>
        /// Parse and verify single item group multiple content Media feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task Media_Single_Item_Group_Multiple_Content() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        /// <summary>
        /// Parse and verify single item extended Media feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task Media_Single_Item_Extended() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);
    }
}