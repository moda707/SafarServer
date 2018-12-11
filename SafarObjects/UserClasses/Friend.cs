using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SafarObjects.UserClasses
{
    public class Friend
    {
        [BsonId] public ObjectId _id { get; set; }
        public string UserId { get; set; }

        public List<FriendShip> FriendShip { get; set; }


        public Friend()
        {
            
        }

    }

    public class FriendShip
    {
        public string UserId { get; set; }
        public FriendShipStatus Status { get; set; }

        public FriendShip(string userId, FriendShipStatus status)
        {
            UserId = userId;
            Status = status;
        }
    }

    public enum FriendShipStatus
    {
        RequestSent = 0,
        RequestAccepted = 1,
        RequestReceived = 2

    }
}
