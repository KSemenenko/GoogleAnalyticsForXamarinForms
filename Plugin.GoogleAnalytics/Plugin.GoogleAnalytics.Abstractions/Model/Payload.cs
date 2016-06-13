using System;
using System.Collections.Generic;

namespace Plugin.GoogleAnalytics.Abstractions.Model
{
    public sealed class Payload
    {
        public Payload(IDictionary<string, string> data)
        {
            Data = data;
            TimeStamp = DateTimeOffset.UtcNow;
        }

        public IDictionary<string, string> Data { get; private set; }
        public DateTimeOffset TimeStamp { get; private set; }
        public bool IsUseSecure { get; set; }
        public bool IsDebug { get; set; }
    }
}