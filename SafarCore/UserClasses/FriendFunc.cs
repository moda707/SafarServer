using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using SafarCore.DbClasses;
using SafarCore.GenFunctions;
using SafarObjects.UserClasses;

namespace SafarCore.UserClasses
{
    public class FriendFunc : Friend
    {
        public static FuncResult SendRequest(string fromUserId, string toUserId)
        {
            try
            {
                var fromFriendship = new FriendShip(toUserId, FriendShipStatus.RequestSent);
                var toFriendship = new FriendShip(fromUserId, FriendShipStatus.RequestReceived);

                var dbConnection = new DbConnection();
            

                //add fromFriendship to fromUser
                var filter = new List<FieldFilter>()
                {
                    new FieldFilter("UserId", fromUserId, FieldType.String, CompareType.Equal)
                };

                var f = dbConnection.GetFilteredList<FriendInDb>(CollectionNames.Friends, filter);
                if (f.Count == 0)
                {
                    var fromUser = new FriendInDb()
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
                    new FieldFilter("UserId", toUserId, FieldType.String, CompareType.Equal)
                };

                f = dbConnection.GetFilteredList<FriendInDb>(CollectionNames.Friends, filter);
                if (f.Count == 0)
                {
                    var toUser = new FriendInDb()
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

                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(fromUserId, nameof(FriendFunc), nameof(SendRequest), e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }
        }

        public static FuncResult AcceptRequest(string currentUserId, string senderUserId)
        {
            try
            {
                var dbConnection = new DbConnection();
            

                //update currentUser
                var filter = new List<FieldFilter>()
                {
                    new FieldFilter("UserId", currentUserId, FieldType.String, CompareType.Equal)
                };
                var user = dbConnection.GetFilteredList<FriendInDb>(CollectionNames.Friends, filter)[0];

                user.FriendShip.First(x => x.UserId == senderUserId).Status = FriendShipStatus.RequestAccepted;
                var col = dbConnection.GetMongoCollection(CollectionNames.Friends);
                dbConnection.AddorUpdate(user.ToBsonDocument(), col, new List<string>() {"UserId"});


                //update senderUser
                filter = new List<FieldFilter>()
                {
                    new FieldFilter("UserId", senderUserId, FieldType.String, CompareType.Equal)
                };
                user = dbConnection.GetFilteredList<FriendInDb>(CollectionNames.Friends, filter)[0];
            
                user.FriendShip.First(x => x.UserId == currentUserId).Status = FriendShipStatus.RequestAccepted;
            
                dbConnection.AddorUpdate(user.ToBsonDocument(), col, new List<string>() { "UserId" });

                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(currentUserId, nameof(FriendFunc), nameof(AcceptRequest), e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }
        }

        public static FuncResult CancelorRejectRequest(string fromUserId, string toUserId)
        {
            try
            {
                var dbConnection = new DbConnection();


                //update currentUser
                var filter = new List<FieldFilter>()
                {
                    new FieldFilter("UserId", fromUserId, FieldType.String, CompareType.Equal)
                };
                var user = dbConnection.GetFilteredList<FriendInDb>(CollectionNames.Friends, filter)[0];

                user.FriendShip.First(x => x.UserId == toUserId).Status = FriendShipStatus.CancelOrRejected;
                var col = dbConnection.GetMongoCollection(CollectionNames.Friends);
                dbConnection.AddorUpdate(user.ToBsonDocument(), col, new List<string>() { "UserId" });


                //update senderUser
                filter = new List<FieldFilter>()
                {
                    new FieldFilter("UserId", toUserId, FieldType.String, CompareType.Equal)
                };
                user = dbConnection.GetFilteredList<FriendInDb>(CollectionNames.Friends, filter)[0];

                user.FriendShip.First(x => x.UserId == fromUserId).Status = FriendShipStatus.CancelOrRejected;

                dbConnection.AddorUpdate(user.ToBsonDocument(), col, new List<string>() { "UserId" });

                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(fromUserId, nameof(FriendFunc), nameof(CancelorRejectRequest), e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }
        }
    }
}
