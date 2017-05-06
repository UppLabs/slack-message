using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SlackMessaging
{
    public class SlackMessage
    {
        public string PostUrl { get; set; }
        public string Text { get; set; }
        public string Channel { get; set; }
        public string Icon_Emoji { get; set; }
        public string Username { get; set; }
        private string PostData
        {
            get
            {
                StringBuilder postData = new StringBuilder();
                postData.Append("payload={");

                if (!String.IsNullOrEmpty(this.Text))
                {
                    postData.Append("\"text\":\"" + this.Text + "\"");
                }

                if (!String.IsNullOrEmpty(this.Channel))
                {
                    postData.Append(",\"channel\":\"" + this.Channel + "\"");
                }

                if (!String.IsNullOrEmpty(this.Icon_Emoji))
                {
                    postData.Append(",\"icon_emoji\":\"" + this.Icon_Emoji + "\"");
                }

                if (!String.IsNullOrEmpty(this.Username))
                {
                    postData.Append(",\"username\": \"" + this.Username + "\"");
                }

                postData.Append("}");
                return postData.ToString();
            }
        }

        /// <summary>
        /// Constructor for the Message class
        /// </summary>
        /// <param name="postUrl">The Webhook URL</param>
        /// <param name="text">The message to send</param>
        /// <param name="channel">The channel name if you want to send the message to a different channel</param>
        /// <param name="icon_emoji">The emoji to use for the icon of the BOT user</param>
        /// <param name="username">The name to use for the BOT user</param>
        public SlackMessage(string postUrl, string text, string channel = null, string icon_emoji = null, string username = null)
        {
            PostUrl = postUrl;
            Text = text;
            Channel = channel;
            Icon_Emoji = icon_emoji;
            Username = username;
        }

        /// <summary>
        /// Calls the method to process the web request
        /// </summary>
        /// <returns>A string value for the response from the server</returns>
        public string Send()
        {
            return ProcessRequest(this.PostUrl, this.PostData);
        }

        /// <summary>
        /// Simple method for processing web requests
        /// </summary>
        /// <param name="postUrl">The url to post to</param>
        /// <param name="postData">The data to post in the web request</param>
        /// <returns>A string value for the response from the server</returns>
        private string ProcessRequest(string postUrl, string postData)
        {
            WebRequest request = WebRequest.Create(postUrl);
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
