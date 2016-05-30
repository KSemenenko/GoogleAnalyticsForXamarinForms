namespace GoogleAnalytics.Core
{
    public interface IServiceManager
    {
        string UserAgent { get; set; }
        void SendPayload(Payload payload);
    }
}