using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Plugin.GoogleAnalytics.Abstractions;

namespace Plugin.GoogleAnalytics
{
    public sealed class GAServiceManager : IServiceManager
    {
        private static Random random;
        private static GAServiceManager current;
        private static readonly Uri endPointUnsecureDebug = new Uri("http://www.google-analytics.com/debug/collect");
        private static readonly Uri endPointSecureDebug = new Uri("https://ssl.google-analytics.com/debug/collect");
        private static readonly Uri endPointUnsecure = new Uri("http://www.google-analytics.com/collect");
        private static readonly Uri endPointSecure = new Uri("https://ssl.google-analytics.com/collect");
        private readonly IList<Task> dispatchingTasks;
        private readonly Queue<Payload> payloads;
        private TimeSpan dispatchPeriod;
        private bool isConnected = true; // assume true. The app can tell us differently
        private Timer timer;

        private GAServiceManager()
        {
            PostData = true;
            dispatchingTasks = new List<Task>();
            payloads = new Queue<Payload>();
            DispatchPeriod = TimeSpan.Zero;
        }

        /// <summary>
        ///     Gets or sets whether data should be sent via POST or GET method. Default is POST.
        /// </summary>
        public bool PostData { get; set; }

        public bool BustCache { get; set; }

        public static GAServiceManager Current
        {
            get
            {
                if(current == null)
                {
                    current = new GAServiceManager();
                }
                return current;
            }
        }

        public TimeSpan DispatchPeriod
        {
            get { return dispatchPeriod; }
            set
            {
                if(dispatchPeriod != value)
                {
                    dispatchPeriod = value;
                    if(timer != null)
                    {
                        timer.Dispose();

                        timer = null;
                    }
                    if(dispatchPeriod > TimeSpan.Zero)
                    {
                        timer = new Timer(timer_Tick, null, DispatchPeriod, DispatchPeriod);
                    }
                }
            }
        }

        public bool IsConnected
        {
            get { return isConnected; }
            set
            {
                if(isConnected != value)
                {
                    isConnected = value;
                    if(isConnected)
                    {
                        if(DispatchPeriod >= TimeSpan.Zero)
                        {
                            var nowait = Dispatch();
                        }
                    }
                }
            }
        }

        public async void SendPayload(Payload payload)
        {
            if(DispatchPeriod == TimeSpan.Zero && IsConnected)
            {
                await RunDispatchingTask(DispatchImmediatePayload(payload));
            }
            else
            {
                lock(payloads)
                {
                    payloads.Enqueue(payload);
                }
            }
        }

        public string UserAgent { get; set; }
        public event EventHandler<PayloadSentEventArgs> PayloadSent;
        public event EventHandler<PayloadFailedEventArgs> PayloadFailed;
        public event EventHandler<PayloadMalformedEventArgs> PayloadMalformed;

        public void Clear()
        {
            lock(payloads)
            {
                payloads.Clear();
            }
        }

        private async void timer_Tick(object sender)
        {
            await Dispatch();
        }

        public async Task Dispatch()
        {
            if(!isConnected)
            {
                return;
            }

            Task allDispatchingTasks = null;
            lock(dispatchingTasks)
            {
                if(dispatchingTasks.Any())
                {
                    allDispatchingTasks = Task.WhenAll(dispatchingTasks);
                }
            }

            if(allDispatchingTasks != null)
            {
                await allDispatchingTasks;
            }

            if(!isConnected)
            {
                return;
            }

            IList<Payload> payloadsToSend = new List<Payload>();
            lock(payloads)
            {
                while(payloads.Count > 0)
                {
                    payloadsToSend.Add(payloads.Dequeue());
                }
            }

            if(payloadsToSend.Any())
            {
                await RunDispatchingTask(DispatchQueuedPayloads(payloadsToSend));
            }
        }

        private async Task RunDispatchingTask(Task newDispatchingTask)
        {
            lock(dispatchingTasks)
            {
                dispatchingTasks.Add(newDispatchingTask);
            }

            try
            {
                await newDispatchingTask;
            }
            finally
            {
                lock(dispatchingTasks)
                {
                    dispatchingTasks.Remove(newDispatchingTask);
                }
            }
        }

        private async Task DispatchQueuedPayloads(IEnumerable<Payload> payloads)
        {
            using(var httpClient = GetHttpClient())
            {
                var now = DateTimeOffset.UtcNow;
                foreach(var payload in payloads)
                {
                    if(isConnected)
                    {
                        // clone the data
                        var payloadData = payload.Data.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                        payloadData.Add("qt", ((long)now.Subtract(payload.TimeStamp).TotalMilliseconds).ToString());
                        await DispatchPayloadData(payload, httpClient, payloadData);
                    }
                    else
                    {
                        lock(payloads) // add back to queue
                        {
                            this.payloads.Enqueue(payload);
                        }
                    }
                }
            }
        }

        private async Task DispatchImmediatePayload(Payload payload)
        {
            using(var httpClient = GetHttpClient())
            {
                // clone the data
                var payloadData = payload.Data.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                await DispatchPayloadData(payload, httpClient, payloadData);
            }
        }

        private async Task DispatchPayloadData(Payload payload, HttpClient httpClient, Dictionary<string, string> payloadData)
        {
            if(BustCache)
            {
                payloadData.Add("z", GetCacheBuster());
            }
            try
            {
                using(var response = await SendPayloadAsync(payload, httpClient, payloadData))
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();
                        await OnPayloadSentAsync(payload, response);
                    }
                    catch // If you do not get a 2xx status code, you should NOT retry the request. Instead, you should stop and correct any errors in your HTTP request.
                    {
                        OnMalformedPayload(payload, response);
                    }
                }
            }
            catch(Exception ex)
            {
                OnPayloadFailed(payload, ex);
            }
        }

        private async Task<HttpResponseMessage> SendPayloadAsync(Payload payload, HttpClient httpClient, Dictionary<string, string> payloadData)
        {
            var endPoint = payload.IsDebug ? endPointSecureDebug : (payload.IsUseSecure ? endPointSecure : endPointUnsecure);
            if(PostData)
            {
                using(var content = GetEncodedContent(payloadData))
                {
                    return await httpClient.PostAsync(endPoint, content);
                }
            }
            return await httpClient.GetAsync(endPoint + "?" + GetUrlEncodedString(payloadData));
        }

        private void OnMalformedPayload(Payload payload, HttpResponseMessage response)
        {
            PayloadMalformed?.Invoke(this, new PayloadMalformedEventArgs(payload, (int)response.StatusCode));
        }

        private void OnPayloadFailed(Payload payload, Exception exception)
        {
            // TODO: store in isolated storage and retry next session
            PayloadFailed?.Invoke(this, new PayloadFailedEventArgs(payload, exception.Message));
        }

        private async Task OnPayloadSentAsync(Payload payload, HttpResponseMessage response)
        {
            PayloadSent?.Invoke(this, new PayloadSentEventArgs(payload, await response.Content.ReadAsStringAsync()));
        }

        private HttpClient GetHttpClient()
        {
            var result = new HttpClient();
            if(!string.IsNullOrEmpty(UserAgent))
            {
                result.DefaultRequestHeaders.UserAgent.ParseAdd(UserAgent);
            }
            return result;
        }

        private static string GetCacheBuster()
        {
            if(random == null)
            {
                random = new Random();
            }
            return random.Next().ToString();
        }

        private static ByteArrayContent GetEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            return new StringContent(GetUrlEncodedString(nameValueCollection));
        }

        private static string GetUrlEncodedString(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            const int MaxUriStringSize = 65519;

            var result = new StringBuilder();
            var isFirst = true;
            foreach(var item in nameValueCollection)
            {
                var value = item.Value;
                if(value != null)
                {
                    if(isFirst)
                    {
                        isFirst = false;
                    }
                    else
                    {
                        result.Append("&");
                    }
                    result.Append(item.Key);
                    result.Append("=");
                    if(value.Length > MaxUriStringSize)
                    {
                        value = value.Substring(0, MaxUriStringSize);
                    }
                    result.Append(Uri.EscapeDataString(value));
                }
            }
            return result.ToString();
        }
    }
}