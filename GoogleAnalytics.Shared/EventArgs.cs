using System;
using Plugin.GoogleAnalytics.Abstractions.Model;
#if ANDROID
using Android.Runtime;
#endif

#if __IOS__ || __MACOS__
using Foundation;
#endif

namespace Plugin.GoogleAnalytics
{
#if !WINDOWS_UWP
    [Preserve(AllMembers = true)]
#endif
    public sealed class PayloadFailedEventArgs : EventArgs
    {
        public PayloadFailedEventArgs(Payload payload, string error)
        {
            Error = error;
            Payload = payload;
        }

        public string Error { get; private set; }
        public Payload Payload { get; private set; }
    }

#if !WINDOWS_UWP
    [Preserve(AllMembers = true)]
#endif
    public sealed class PayloadSentEventArgs : EventArgs
    {
        public PayloadSentEventArgs(Payload payload, string response)
        {
            Response = response;
            Payload = payload;
        }

        public string Response { get; private set; }
        public Payload Payload { get; private set; }
    }

#if !WINDOWS_UWP
    [Preserve(AllMembers = true)]
#endif
    public sealed class PayloadMalformedEventArgs : EventArgs
    {
        public PayloadMalformedEventArgs(Payload payload, int httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
            Payload = payload;
        }

        public int HttpStatusCode { get; private set; }
        public Payload Payload { get; private set; }
    }
}