using System;
using SafarObjects.ChatsClasses;
using SafarSDK.Core;

namespace SafarSDK.Models
{
    public class ChatMessageModel : ModelBase
    {
        public string MessageId { get; set; }
        public string TripId { get; set; }
        public string MessageText { get; set; }
        public string FromName { get; set; }
        public string FromId { get; set; }
        public string UriLink { get; set; }
        public ChatMessageType MessageType { get; set; }
        public ChatMessageStatus MessageStatus { get; set; }
        public ChatMessageSeenStatus MessageSeenStatus { get; set; }
        public DateTime MessageDate { get; set; }

        public ChatMessageModel()
        {

        }


    }
}
