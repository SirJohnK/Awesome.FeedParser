using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Helpers;

namespace Tests
{
    /// <summary>
    /// Tests for different Atom feeds.
    /// </summary>
    [TestClass]
    public class AtomTests
    {
        private const string ResourceId = "Atom_";
        private Dictionary<string, (string name, string file)> resources;

        /// <summary>
        /// Setup and get Atom resources.
        /// </summary>
        public AtomTests()
        {
            //Get Atom Resources
            resources = Resources.GetResources(ResourceId);
        }

        /// <summary>
        /// Parse and verify basic Atom feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task Atom_Basic() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        /// <summary>
        /// Parse and verify extended Atom feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task Atom_Extended() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);

        /// <summary>
        /// Parse and verify second extended Atom feed.
        /// </summary>
        /// <returns>The test method task.</returns>
        [TestMethod]
        public async Task Atom_Extended_II() => await TestHelpers.ParseResource(MethodHelpers.GetName(), resources);
    }
}