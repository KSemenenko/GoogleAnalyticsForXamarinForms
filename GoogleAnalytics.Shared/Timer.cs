using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.GoogleAnalytics
{
    public delegate void TimerCallback(object state);

    public sealed class Timer : CancellationTokenSource, IDisposable
    {
        public Timer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period): this(callback, state, (int)dueTime.TotalMilliseconds, (int)period.TotalMilliseconds)
        {
        }

        public Timer(TimerCallback callback, object state, int dueTime, int period)
        {
            Task.Run(() => WaitTimer(callback, state, period, Token));
        }

        public new void Dispose()
        {
            Cancel();
        }

        private static async Task WaitTimer(TimerCallback callback, object state, int period, CancellationToken token)
        {
            await WaitPeriod(callback, state, period, token);
            if(period > 0)
            {
                while(!token.IsCancellationRequested)
                {
                    await WaitPeriod(callback, state, period, token);
                }
            }
        }

        private static async Task WaitPeriod(TimerCallback callback, object state, int period, CancellationToken token)
        {
            try
            {
                await Task.Delay(period, token);
                callback(state);
            }
            catch(TaskCanceledException)
            {
                // ignore 
            }
        }
    }
}