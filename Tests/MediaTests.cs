using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class MediaTests
    {
        private const string ResourceId = "Media_";
        private Dictionary<string, (string name, string file)> resources;

        public MediaTests()
        {
            //Get Media Resources
            resources = Resources.GetResources(ResourceId);
        }

        [TestMethod]
        public async Task Media_Single_Item_Content() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        [TestMethod]
        public async Task Media_Single_Item_Multiple_Content() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        [TestMethod]
        public async Task Media_Single_Item_Group_Multiple_Content() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        [TestMethod]
        public async Task Media_Single_Item_Extended() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);
    }
}