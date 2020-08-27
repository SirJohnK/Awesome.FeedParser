using System.Collections.Generic;

namespace Awesome.FeedParser.Extensions
{
    /// <summary>
    /// Internal Dictionary extension methods.
    /// </summary>
    internal static class DictionaryExtensions
    {
        /// <summary>
        /// Add same key and value types dictionary to current dictionary.
        /// </summary>
        /// <typeparam name="TKey">Dictionary key type.</typeparam>
        /// <typeparam name="TValue">Dictionary value type.</typeparam>
        /// <param name="dictionary">Current dictionary.</param>
        /// <param name="collection">Dictionary to add to current dictionary.</param>
        /// <param name="replace">Flag inticating if duplicate key values should be replaced. (Default: false)</param>
        internal static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> collection, bool replace = false)
        {
            foreach (var element in collection)
            {
                if (!dictionary.ContainsKey(element.Key))
                    dictionary.Add(element.Key, element.Value);
                else if (replace)
                    dictionary[element.Key] = element.Value;
            }
        }

        /// <summary>
        /// Add key list with one value to current dictionary.
        /// </summary>
        /// <typeparam name="TKey">Dictionary key type.</typeparam>
        /// <typeparam name="TValue">Dictionary value type.</typeparam>
        /// <param name="dictionary">Current dictionary.</param>
        /// <param name="keys">List of keys.</param>
        /// <param name="value">Value to add for each key to current dictionary.</param>
        /// <param name="replace">Flag inticating if duplicate key values should be replaced. (Default: false)</param>
        internal static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys, TValue value, bool replace = false)
        {
            foreach (var key in keys)
            {
                if (!dictionary.ContainsKey(key))
                    dictionary.Add(key, value);
                else if (replace)
                    dictionary[key] = value;
            }
        }
    }
}