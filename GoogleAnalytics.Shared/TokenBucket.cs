using System;

namespace Plugin.GoogleAnalytics
{
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