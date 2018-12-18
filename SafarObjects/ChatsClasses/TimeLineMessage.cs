using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarObjects.TripClasses;

namespace SafarObjects.ChatsClasses
{
    public class TimelineMessage
    {
        public string TimelineId { get; set; }
        public string MessageId { get; set; }
        public string MessageText { get; set; }
        public string FromName { get; set; }
        public string FromId { get; set; }
        public string UriLink { get; set; }
        public TimelineMessageType TimelineMessageType { get; set; }
        public TimelineMessageStatus TimelineMessageStatus { get; set; }
        public TimelinePrivacyType TimelineMessagePrivacyType { get; set; }
        public DateTime MessageDate { get; set; }
        public int SeenCount { get; set; }
        public int LikedCount { get; set; }
        public int SharedCount { get; set; }

        public GeoPoint GeoLocation { get; set; }

        public TimelineMessageInDb GetTimelineMessageInDb()
        {
            return new TimelineMessageInDb()
            {
                TimelineId = this.TimelineId,
                FromId = this.FromId,
                FromName = this.FromName,
                MessageId = this.MessageId,
                MessageText = this.MessageText,
                GeoLocation = this.GeoLocation,
                MessageDate = this.MessageDate,
                UriLink = this.UriLink,
                LikedCount = this.LikedCount,
                SeenCount = this.SeenCount,
                SharedCount = this.SharedCount,
                TimelineMessagePrivacyType = this.TimelineMessagePrivacyType,
                TimelineMessageStatus = this.TimelineMessageStatus,
                TimelineMessageType = this.TimelineMessageType
            };
        }
    }

    public class TimelineMessageInDb : TimelineMessage
    {
        [BsonId]
        public ObjectId _id { get; set; }
        

        public TimelineMessageInDb()
        {
            
        }

        public TimelineMessage GetTimelineMessage()
        {
            return new TimelineMessage()
            {
                TimelineId = this.TimelineId,
                FromId = this.FromId,
                FromName = this.FromName,
                MessageId = this.MessageId,
                MessageText = this.MessageText,
                GeoLocation = this.GeoLocation,
                MessageDate = this.MessageDate,
                UriLink = this.UriLink,
                LikedCount = this.LikedCount,
                SeenCount = this.SeenCount,
                SharedCount = this.SharedCount,
                TimelineMessagePrivacyType = this.TimelineMessagePrivacyType,
                TimelineMessageStatus = this.TimelineMessageStatus,
                TimelineMessageType = this.TimelineMessageType
            };
        }
    }

    public enum TimelineMessageType
    {
        Text = 0,
        TextImage = 1,
        Image = 2
    }

    public enum TimelineMessageStatus
    {
        Original = 0,
        Edited = 1,
        Deleted = 2
    }

    public enum TimelinePrivacyType
    {
        Private = 0,
        PublicForFriends = 1,
        PublicForEveryOne = 2
    }

    public class Timeline
    {
        [BsonId] public ObjectId _id { get; set; }
        public string TimelineId { get; set; }
        public string TripId { get; set; }
        public TimelinePrivacyType TimelinePrivacy { get; set; }

        public Timeline()
        {
            
        }
    }
}
