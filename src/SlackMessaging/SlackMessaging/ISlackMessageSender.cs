namespace SlackMessaging
{
    public interface ISlackMessageSender
    {
        void Send(string message);
    }
}