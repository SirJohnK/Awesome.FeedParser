using System.Runtime.CompilerServices;

namespace Tests.Helpers
{
    /// <summary>
    /// Custom method helpers.
    /// </summary>
    internal static class MethodHelpers
    {
        /// <summary>
        /// Returns current caller member name.
        /// </summary>
        /// <param name="memberName">Current caller member name.</param>
        /// <returns>The caller member name.</returns>
        internal static string GetName([CallerMemberName] string memberName = "") => memberName;
    }
}