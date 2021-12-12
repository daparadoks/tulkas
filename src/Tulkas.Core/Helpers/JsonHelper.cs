using Newtonsoft.Json;

namespace Tulkas.Core.Helpers
{
    public class JsonHelper
    {
        public static string Serialize<T>(T value) => JsonConvert.SerializeObject(value);

        public static T Deserialize<T>(string value) =>
            string.IsNullOrEmpty(value) ? default : JsonConvert.DeserializeObject<T>(value);
    }
}