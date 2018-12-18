using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SafarObjects.ChatsClasses
{
    public class ChatMessage
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

        public ChatMessage()
        {
            
        }

        public ChatMessageInDb GetChatMessageInDb()
        {
            return new ChatMessageInDb()
            {
                MessageId = this.MessageId,
                FromId = this.FromId,
                FromName = this.FromName,
                TripId = this.TripId,
                MessageText = this.MessageText,
                UriLink = this.UriLink,
                MessageDate = this.MessageDate,
                MessageType = this.MessageType,
                MessageSeenStatus = this.MessageSeenStatus,
                MessageStatus = this.MessageStatus
            };
        }
    }


    public class ChatMessageInDb : ChatMessage
    {
        [BsonId]
        public ObjectId _id { get; set; }
       
        public ChatMessageInDb()
        {
            
        }

        public ChatMessage GetChatMessage()
        {
            return new ChatMessage()
            {
                MessageId = this.MessageId,
                FromId = this.FromId,
                FromName = this.FromName,
                TripId = this.TripId,
                MessageText = this.MessageText,
                UriLink = this.UriLink,
                MessageDate = this.MessageDate,
                MessageType = this.MessageType,
                MessageSeenStatus = this.MessageSeenStatus,
                MessageStatus = this.MessageStatus
            };
        }
    }
    
    public class ChatMessageShort
    {
        public string FromName { get; set; }
        public string MessageText { get; set; }

        public ChatMessageShort(string fromName, string messageText)
        {
            FromName = fromName;
            MessageText = messageText;
        }
    }

    public class ImageInfo
    {
        public string TripId { get; set; }
        public string MessageId { get; set; }
        public MediaType MediaType { get; set; }
    }

    public enum MediaType
    {
        Image = 0,
        Video = 1
    }
}
