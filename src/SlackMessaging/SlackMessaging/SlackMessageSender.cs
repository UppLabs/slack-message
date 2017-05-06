using System.Configuration;

namespace SlackMessaging
{
    public class SlackMessageSender : ISlackMessageSender
    {
        public void Send(string message)
        {
            string webhookUrl = ConfigurationManager.AppSettings["slack:WebhookUrl"];
            string channelName = ConfigurationManager.AppSettings["slack:ChannelName"];
            string emojiIcon = ":computer:";
            string username = ConfigurationManager.AppSettings["slack:Username"];

            SlackMessage slackMessage = new SlackMessage(webhookUrl, message, channelName, emojiIcon, username);
            slackMessage.Send();
        }
    }
}
