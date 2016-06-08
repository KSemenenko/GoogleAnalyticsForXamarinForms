namespace Plugin.GoogleAnalytics.Abstractions
{
    public interface IServiceManager
    {
        string UserAgent { get; set; }
        void SendPayload(Payload payload);
    }
}