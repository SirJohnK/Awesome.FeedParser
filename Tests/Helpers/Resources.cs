using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Tests.Helpers
{
    /// <summary>
    /// Custom tests resources handling.
    /// </summary>
    internal static class Resources
    {
        private static Assembly assembly = Assembly.GetExecutingAssembly();

        /// <summary>
        /// Get all resources based on specified resource id.
        /// </summary>
        /// <param name="resourceId">The resource id.</param>
        /// <returns>A dictionary with name and file information for resources based on the specified resource id</returns>
        internal static Dictionary<string, (string name, string file)> GetResources(string resourceId)
        {
            //Get resources, starting with resource id
            var names = assembly.GetManifestResourceNames();
            return names.Where(name => name.Contains(resourceId))
                .Select(name =>
                {
                    var startIndex = name.LastIndexOf(resourceId);
                    return (name, file: name.Substring(startIndex));
                })
                .ToDictionary(resource =>
                {
                    var extension = Path.GetExtension(resource.file);
                    return resource.file.Substring(0, resource.file.Length - extension.Length);
                });
        }

        /// <summary>
        /// Get resource stream.
        /// </summary>
        /// <param name="resource">Resource name.</param>
        /// <returns></returns>
        internal static Stream GetResource(string resource) => assembly.GetManifestResourceStream(resource);

        /// <summary>
        /// Returns Resources location path.
        /// </summary>
        internal static string Location => Path.Combine(Directory.GetParent(assembly.Location).Parent.Parent.Parent.FullName, "Resources");
    }
}