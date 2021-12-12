using System.Collections.Generic;

namespace Tulkas.Core.Constants
{
    public static class ExceptionConstants
    {
        public static KeyValuePair<int, string> Required(string message) =>
            new KeyValuePair<int, string>(400, $"{message} gerekmektedir.");
        public static KeyValuePair<int, string> NotFound(string message) =>
            new KeyValuePair<int, string>(404, $"{message} bulunamadı.");

        public static KeyValuePair<int, string> ActionFailed(string message) =>
            new KeyValuePair<int, string>(500, $"{message} işlemi tamamlanamadı.");
        
        public static KeyValuePair<int, string> InvalidRequest() =>
            new KeyValuePair<int, string>(400, $"Geçersiz bir talepte bulundunuz.");
    }
}