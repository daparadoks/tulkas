using System;
using System.Collections.Generic;

namespace Tulkas.Core.Components
{
    public class CustomInformationException:Exception
    {
        public CustomInformationException(string message, int code = 200, bool success = false): base(message)
        {
            Success = success;
            Code = code;
        }

        public CustomInformationException(KeyValuePair<int, string> message, bool success = false):base(message.Value)
        {
            Code = message.Key;
            Success = success;
        }
        public bool Success { get; set; }
        public int Code { get; set; }
    }
}