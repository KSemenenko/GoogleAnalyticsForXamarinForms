using System;
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
    public class TokenBucket
    {
        private readonly double capacity;
        private readonly double fillRate;
        private readonly object sync = new object();
        private DateTime timeStamp;
        private double tokens;

        public TokenBucket(double tokens, double fillRate)
        {
            capacity = tokens;
            this.tokens = tokens;
            this.fillRate = fillRate;
            timeStamp = DateTime.Now;
        }

        public bool Consume(double tokens = 1.0)
        {
            lock(sync)
            {
                if(GetTokens() - tokens > 0)
                {
                    this.tokens -= tokens;
                    return true;
                }
                return false;
            }
        }

        private double GetTokens()
        {
            var now = DateTime.Now;
            if(tokens < capacity)
            {
                var delta = fillRate*(now - timeStamp).TotalSeconds;
                tokens = Math.Min(capacity, tokens + delta);
                timeStamp = now;
            }
            return tokens;
        }
    }
}