using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarCore.DbClasses;
using SafarCore.GenFunctions;

namespace SafarCore.UserClasses
{
    public class Friend
    {
        [BsonId]
        public ObjectId UserId { get; set; }

        public List<FriendShip> FriendShip { get; set; }


        public Friend()
        {
            
        }

        public static FuncResult SendRequest(ObjectId fromUserId, ObjectId toUserId)
        {
            var fromFriendship = new FriendShip(toUserId, UserClasses.FriendShipStatus.RequestSent);
            var toFriendship = new FriendShip(fromUserId, UserClasses.FriendShipStatus.RequestReceived);

            var dbConnection = new DbConnection();
            dbConnection.ConnectOpenReg();

            //add fromFriendship to fromUser
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", fromUserId, FieldType.ObjectId, CompareType.Equal)
            };

            var f = dbConnection.GetFilteredList<Friend>(CollectionNames.Friends, filter);
            if (f.Count == 0)
            {
                var fromUser = new Friend()
                {
                    UserId = fromUserId,
                    FriendShip = new List<FriendShip>()
                    {
                        fromFriendship
                    }
                };
                var col = dbConnection.GetMongoCollection(CollectionNames.Friends);
                dbConnection.AddorUpdate(fromUser.ToBsonDocument(), col);
            }
            else
            {
                f[0].FriendShip.Add(fromFriendship);
                var col = dbConnection.GetMongoCollection(CollectionNames.Friends);
                dbConnection.AddorUpdate(f[0].ToBsonDocument(), col, new List<string>() {"UserId"});
            }

            //add toFriendship to toUser
            filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", toUserId, FieldType.ObjectId, CompareType.Equal)
            };

            f = dbConnection.GetFilteredList<Friend>(CollectionNames.Friends, filter);
            if (f.Count == 0)
            {
                var toUser = new Friend()
                {
                    UserId = toUserId,
                    FriendShip = new List<FriendShip>()
                    {
                        toFriendship
                    }
                };
                var col = dbConnection.GetMongoCollection(CollectionNames.Friends);
                dbConnection.AddorUpdate(toUser.ToBsonDocument(), col);
            }
            else
            {
                f[0].FriendShip.Add(toFriendship);
                var col = dbConnection.GetMongoCollection(CollectionNames.Friends);
                dbConnection.AddorUpdate(f[0].ToBsonDocument(), col, new List<string>() { "UserId" });
            }

            return FuncResult.Successful;
        }

        public static FuncResult AcceptRequest(ObjectId currentUserId, ObjectId senderUserId)
        {
            var dbConnection = new DbConnection();
            dbConnection.ConnectOpenReg();

            //update currentUser
            var filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", currentUserId, FieldType.ObjectId, CompareType.Equal)
            };
            var user = dbConnection.GetFilteredList<Friend>(CollectionNames.Friends, filter)[0];

            user.FriendShip.First(x => x.UserId == senderUserId).Status = FriendShipStatus.RequestAccepted;
            var col = dbConnection.GetMongoCollection(CollectionNames.Friends);
            dbConnection.AddorUpdate(user.ToBsonDocument(), col, new List<string>() {"UserId"});


            //update senderUser
            filter = new List<FieldFilter>()
            {
                new FieldFilter("UserId", senderUserId, FieldType.ObjectId, CompareType.Equal)
            };
            user = dbConnection.GetFilteredList<Friend>(CollectionNames.Friends, filter)[0];
            
            user.FriendShip.First(x => x.UserId == currentUserId).Status = FriendShipStatus.RequestAccepted;
            
            dbConnection.AddorUpdate(user.ToBsonDocument(), col, new List<string>() { "UserId" });

            return FuncResult.Successful;
        }

        public static FuncResult CancelorRejectRequest(ObjectId fromUserId, ObjectId toUserId)
        {

            return FuncResult.Successful;
        }
    }

    public class FriendShip
    {
        public ObjectId UserId { get; set; }
        public FriendShipStatus Status { get; set; }

        public FriendShip(ObjectId userId, FriendShipStatus status)
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
