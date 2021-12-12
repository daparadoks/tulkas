using System.Collections.Generic;

namespace Tulkas.Core.Constants
{
    public static class CacheKeysConstants
    {
        public static KeyValuePair<string, int> Product(string id) => new KeyValuePair<string, int>($"Product_{id}", 5);
        public static KeyValuePair<string, int> Category(string id) => new KeyValuePair<string, int>($"Category_{id}", 5);
    }
}